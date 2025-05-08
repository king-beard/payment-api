using Payment.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
IServiceCollection services = builder.Services.AddOpenApi();
IConfiguration configuration = builder.Configuration;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services
    .AddHealthChecksConfiguration()
    .AddSQLDatabaseConfiguration(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseHealthChecks();

app.Run();