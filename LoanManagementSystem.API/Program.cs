using LoanManagementSystem.Data.Context;
using LoanManagementSystem.Data.Repositories.Interface;
using LoanManagementSystem.Data.Repositories;
using LoanManagementSystem.Service.Impl;
using LoanManagementSystem.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Microsoft.AspNetCore.RateLimiting;


var builder = WebApplication.CreateBuilder(args);

// Register DbContext
builder.Services.AddDbContext<AppDbContext>(options =>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")?? throw new InvalidOperationException("Connection string not found.")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

// Add Controllers (important!)

builder.Services.AddControllers();
// Swagger 
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapControllers();
}
 //api start here..
try
{
   
    app.Run();
}
catch (Exception ex)
{
    throw;
}
finally
{
   
}