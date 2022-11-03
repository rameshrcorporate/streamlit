using Gateway.API.Common;
using Gateway.API.DIResolver;
using Gateway.API.Middleware;
using Gateway.API.Service;
using GRO.SharedLibrary.Middleware;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{env}.json", true, true);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var apiSettings = configuration.GetSection(nameof(ApiSettings)).Get<ApiSettings>();
builder.Services.AddSingleton(apiSettings);

//Configure DIResolver
builder.Services.ConfigureDIResolver();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
//app.UseMiddleware(typeof(GroMiddleware));
app.UseMiddleware<GroMiddleware>();
app.MapControllers();

app.Run();
