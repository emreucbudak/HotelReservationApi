using FluentValidation;
using HotelReservationApi.Application.AutoMapper;
using HotelReservationApi.Application.Behaviors;
using HotelReservationApi.Application.Emails;
using HotelReservationApi.Application.Payment;
using HotelReservationApi.Application.QueueMessaging.CreateReservationQueue.Consumer;
using HotelReservationApi.Application.QueueMessaging.CreateReservationQueue.HostedService;
using HotelReservationApi.Application.QueueMessaging.SendBillsAfterReservation.HostedService;
using HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.Consumer;
using HotelReservationApi.Application.QueueMessaging.TwoFactorQueue.HostedService;
using HotelReservationApi.Application.RabbitMq.Interfaces;
using HotelReservationApi.Application.RabbitMq.Settings;
using HotelReservationApi.Application.Repositories;
using HotelReservationApi.Application.UnitOfWork;
using HotelReservationApi.Application.Validate;
using HotelReservationApi.Domain.Entities;
using HotelReservationApi.Infrastructure.Emails;
using HotelReservationApi.Infrastructure.Payment;
using HotelReservationApi.Infrastructure.RabbitMq;
using HotelReservationApi.Infrastructure.Tokens;
using HotelReservationApi.Persistence.ApplicationContext;
using HotelReservationApi.Persistence.Repositories;
using HotelReservationApi.Persistence.UnitOf;
using HotelReservationApi.Presentation.ExceptionHandler;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using QuestPDF.Infrastructure;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
QuestPDF.Settings.License = LicenseType.Community;
var logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog(logger);
var rabbitMqSettings = builder.Configuration.GetSection("RabbitMqSettings").Get<RabbitMqSettings>();

if (!string.IsNullOrEmpty(rabbitMqSettings.UsernameFile) && File.Exists(rabbitMqSettings.UsernameFile))
{
    rabbitMqSettings.Username = File.ReadAllText(rabbitMqSettings.UsernameFile).Trim();
}

if (!string.IsNullOrEmpty(rabbitMqSettings.PasswordFile) && File.Exists(rabbitMqSettings.PasswordFile))
{

    rabbitMqSettings.Password = File.ReadAllText(rabbitMqSettings.PasswordFile).Trim();
}

builder.Services.AddSingleton(Options.Create(rabbitMqSettings));
builder.Services.AddSingleton<IMessageQueueService, RabbitMqProducer>();
builder.Services.AddSingleton<TwoFactorConsumer>();
builder.Services.AddHostedService<RabbitMqProducer>();
builder.Services.AddHostedService<TwoFactorHostedService>();
builder.Services.AddHostedService<CreateReservationHostedService>();
builder.Services.AddHostedService<SendBillsReservationHostedService>();
builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<TwoFactorAuthSettings>(builder.Configuration.GetSection("TwoFactorAuthSettings"));
var secretStripe = "/run/secrets/stripe_secret_key";
string stripeSecretKey = File.Exists(secretStripe)
    ? File.ReadAllText(secretStripe).Trim()
    : throw new Exception("Stripe secret bulunamadý!");
Stripe.StripeConfiguration.ApiKey = stripeSecretKey;
var jwtSecret = "/run/secrets/jwt_secret";
string jwtSecretKey = File.ReadAllText(jwtSecret).Trim();
var dbSecret = "/run/secrets/db_password";
string dbPassword = File.ReadAllText(dbSecret).Trim();
string connectionString = $"Host=postgresql;Port=5432;Database=HotelReservationDb;Username=postgres;Password={dbPassword}";
var smtpPasswordFile = Environment.GetEnvironmentVariable("SMTP_PASSWORD_FILE");
var smtpPassword = File.Exists(smtpPasswordFile)
    ? File.ReadAllText(smtpPasswordFile).Trim()
    : throw new Exception("SMTP þifresi bulunamadý!");
builder.Configuration["Smtp:Password"] = smtpPassword;
builder.Configuration["Smtp:User"] = Environment.GetEnvironmentVariable("SMTP_USER");
builder.Configuration["Smtp:Host"] = Environment.GetEnvironmentVariable("SMTP_HOST");
builder.Configuration["Smtp:Port"] = Environment.GetEnvironmentVariable("SMTP_PORT");
builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("Smtp"));
builder.Services.AddSingleton<CreateReservationConsumer>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString)
);
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Cache");
});
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerBehavior<,>));
builder.Services.AddAutoMapper(cfg => { },typeof(Profiles).Assembly);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IEmailService,EmailService>();
builder.Services.AddScoped<IStripeService,StripeService>();
builder.Services.AddScoped<HotelReservationApi.Application.Tokens.ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining<AdsBannerValidator>();
builder.Services.AddExceptionHandler<ValidationExceptionsHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionsHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(ops =>
{
    ops.Audience = builder.Configuration["JwtSettings:Audience"];
    ops.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSecretKey)),
        ClockSkew = TimeSpan.Zero,



    };
});
builder.Services.AddIdentity<User, Role>(opt =>
{
    opt.Password.RequireDigit = true;
    opt.Password.RequireLowercase = true;
    opt.Password.RequireUppercase = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequiredLength = 8;

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
builder.Services.AddAuthorization(opt =>
{
    opt.AddPolicy("Verified2FA", policy =>
    {
        policy.RequireClaim("2fa_status", "verified");
    });
    opt.AddPolicy("OnlyPending2FA", policy =>
    {
        policy.RequireClaim("2fa_status", "pending");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        db.Database.Migrate();
    }

}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
