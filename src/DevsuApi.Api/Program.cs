using Carter;
using DevsuApi.Api.Extensions;
using DevsuApi.Features;
using DevsuApi.Infrastructure;
using DevsuApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddFeatures();
builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapCarter();

app.UseHttpsRedirection();

app.Run();

public partial class Program { }
// This partial class is used to allow the test project to access internal members of the API project.