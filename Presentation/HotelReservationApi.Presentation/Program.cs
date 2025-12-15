using FluentValidation;
using HotelReservationApi.Application.AutoMapper;
using HotelReservationApi.Application.Behaviors;
using HotelReservationApi.Application.Emails;
using HotelReservationApi.Application.Payment;
using HotelReservationApi.Application.PdfWriter;
using HotelReservationApi.Application.QueueMessaging.CreateReservationQueue.Consumer;
using HotelReservationApi.Application.QueueMessaging.CreateReservationQueue.HostedService;
using HotelReservationApi.Application.QueueMessaging.SendBillsAfterReservation.Consumer;
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
using HotelReservationApi.Infrastructure.PdfWriter;
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
using Microsoft.OpenApi.Models;
using Npgsql;
using QuestPDF.Infrastructure;
using Serilog;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

QuestPDF.Settings.License = LicenseType.Community;

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog(logger);

/* -------------------- RABBITMQ (DÜZENLENDÝ) -------------------- */
// Artýk dosya yolu ile uðraþmýyoruz, fonksiyon hallediyor
builder.Configuration["RabbitMqSettings:Username"] = GetSecret("rabbitmq_user");
builder.Configuration["RabbitMqSettings:Password"] = GetSecret("rabbitmq_password");

builder.Services.Configure<RabbitMqSettings>(
    builder.Configuration.GetSection("RabbitMqSettings"));

builder.Services.AddSingleton<IMessageQueueService, RabbitMqProducer>();
builder.Services.AddHostedService<RabbitMqProducer>();
builder.Services.AddHostedService<TwoFactorHostedService>();
builder.Services.AddHostedService<CreateReservationHostedService>();
builder.Services.AddHostedService<SendBillsReservationHostedService>();

builder.Services.AddSingleton<TwoFactorConsumer>();
builder.Services.AddSingleton<CreateReservationConsumer>();
builder.Services.AddSingleton<SendBillsReservationConsumer>();

/* -------------------- STRIPE (DÜZENLENDÝ) -------------------- */
// Stripe için özel karakter temizliði (.TrimStart) korundu
Stripe.StripeConfiguration.ApiKey =
    GetSecret("stripe_secret_key").TrimStart('\uFEFF', '\u200B');

/* -------------------- JWT (DÜZENLENDÝ) -------------------- */
var jwtSecretKey = GetSecret("jwt_secret");

/* -------------------- DATABASE (DÜZENLENDÝ) -------------------- */
var dbPassword = GetSecret("db_password");

var connectionString = new NpgsqlConnectionStringBuilder
{
    Host = "postgresql", // Docker service name
    Port = 5432,
    Database = "HotelReservationDb",
    Username = "postgres",
    Password = dbPassword
}.ConnectionString;

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseNpgsql(connectionString));

/* -------------------- SMTP (DÜZENLENDÝ) -------------------- */
builder.Configuration["Smtp:Password"] = GetSecret("smtp_app_pass");
builder.Configuration["Smtp:User"] = Environment.GetEnvironmentVariable("SMTP_USER");
builder.Configuration["Smtp:Host"] = Environment.GetEnvironmentVariable("SMTP_HOST");
builder.Configuration["Smtp:Port"] = Environment.GetEnvironmentVariable("SMTP_PORT");

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Smtp"));

/* -------------------- SERVICES -------------------- */
builder.Services.AddControllers();
builder.Services.AddAutoMapper(cfg => { }, typeof(Profiles).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssemblyContaining<AdsBannerValidator>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStripeService, StripeService>();
builder.Services.AddScoped<HotelReservationApi.Application.Tokens.ITokenService, TokenService>();
builder.Services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
builder.Services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IPdfWriter, PdfWriterService>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerBehavior<,>));

builder.Services.AddExceptionHandler<ValidationExceptionsHandler>();
builder.Services.AddExceptionHandler<NotFoundExceptionsHandler>();
builder.Services.AddProblemDetails();

/* -------------------- AUTH -------------------- */
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(jwtSecretKey)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

/* -------------------- SWAGGER -------------------- */
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "HotelReservation API",
        Version = "v1"
    });
});

/* ================================================= */

var app = builder.Build();

/* -------------------- MIDDLEWARE -------------------- */
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelReservation API v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

/* ================================================= */
/* HELPER METHODS (Secret Okuyucu)          */
/* ================================================= */

/// <summary>
/// Bu fonksiyon hem Docker (/run/secrets) hem de Local (../../secrets)
/// ortamlarýný kontrol edip secret deðerini döndürür.
/// </summary>
string GetSecret(string secretName)
{
    // 1. Önce Docker Secret yoluna bak (Prod ortamý için)
    string dockerSecretPath = $"/run/secrets/{secretName}";
    if (File.Exists(dockerSecretPath))
    {
        return File.ReadAllText(dockerSecretPath).Trim();
    }

    // 2. Bulamazsa Local yola bak (Senin þu anki ../../ yapýn)
    string localSecretPath = $"../../secrets/{secretName}";
    if (File.Exists(localSecretPath))
    {
        return File.ReadAllText(localSecretPath).Trim();
    }

    // 3. Hiçbiri yoksa Environment Variable kontrolü de yapýlabilir (Opsiyonel)
    var envVar = Environment.GetEnvironmentVariable(secretName);
    if (!string.IsNullOrEmpty(envVar))
    {
        return envVar;
    }

    // 4. Hiçbir yerde yoksa Console'a hata basýp boþ dönelim (veya throw edebilirsin)
    Console.WriteLine($"[UYARI] Secret '{secretName}' bulunamadý! (Docker: {dockerSecretPath}, Local: {localSecretPath})");
    return string.Empty;
}