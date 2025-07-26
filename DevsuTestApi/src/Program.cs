using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Define a minimal API endpoint
app.MapGet("/example", () =>
{
    return new ExampleModel { Id = 1, Name = "Sample" };
});

app.Run();

public class ExampleModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}