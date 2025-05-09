using Carter;
using Payment.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
IServiceCollection services = builder.Services.AddOpenApi();
IConfiguration configuration = builder.Configuration;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services
    .AddCarter()
    .AddHealthChecksConfiguration()
    .AddMediatRConfiguration(assembly)
    .AddSQLDatabaseConfiguration(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerWithUi();
}

// app.ApplyMigrations();

//app.UseHttpsRedirection();
app.UseHealthChecks();
app.MapCarter();

app.Run();