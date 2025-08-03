using Carter;
using DevsuApi.Api.Extensions;
using DevsuApi.Features;
using DevsuApi.Infrastructure;
using DevsuApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole(); // logs to stdout
builder.Logging.AddDebug();   // optional for dev/debug

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddFeatures();
builder.Services.AddCarter();

// Bind CORS settings from config
string[] allowedOrigins = builder.Configuration["CORS:AllowedOrigins"]?
    .Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries) ?? []; // Get the CORS settings from docker compose

builder.Services.AddCors(options =>
{
    options.AddPolicy("ConfiguredCors", policy =>
    {
        policy
            .WithOrigins(allowedOrigins) // your Angular frontend origin
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseCors("ConfiguredCors");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapCarter();

app.UseHttpsRedirection();

app.Run();

public partial class Program { }
// This partial class is used to allow the test project to access internal members of the API project.