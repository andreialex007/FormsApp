using System.Reflection;
using FormsApp.Common;
using FormsApp.Core.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("FormsAppDb"));

// Register services
builder.Services.AddScoped<FormsApp.Core.Services.Submission.SubmissionService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    if (!context.Submissions.Any())
    {
        context.Submissions.AddRange(
            new FormsApp.Core.Entities.Submission
            {
                Content = "Sample submission 1",
                Created = DateTime.UtcNow.AddDays(-2)
            },
            new FormsApp.Core.Entities.Submission
            {
                Content = "Sample submission 2",
                Created = DateTime.UtcNow.AddDays(-1)
            }
        );
        context.SaveChanges();
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Discover and map all endpoints
var endpointTypes = Assembly.GetExecutingAssembly()
    .GetTypes()
    .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);

foreach (var endpointType in endpointTypes)
{
    var endpoint = (IEndpoint)Activator.CreateInstance(endpointType)!;
    endpoint.Map(app);
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast");

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
}