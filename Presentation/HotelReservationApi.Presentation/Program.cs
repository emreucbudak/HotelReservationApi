using HotelReservationApi.Application.AutoMapper;
using HotelReservationApi.Persistence.ApplicationContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using System;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.UseNetTopologySuite())
);
builder.Services.AddAutoMapper(cfg => { },typeof(Profiles).Assembly);



var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwagger();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
