using System.Reflection;
using FluentValidation;
using FormsApp.Common;
using FormsApp.Core.Data;
using FormsApp.Core.Data.Entities;
using FormsApp.Core.Services.Submissions;
using FormsApp.Core.Services.Submissions.Dto;
using FormsApp.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FormsApp API",
        Version = "v1"
    });
});
builder.Services.AddProblemDetails();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("FormsAppDb"));
builder.Services.AddValidatorsFromAssemblyContaining<SubmissionSearchDto>();
builder.Services.AddScoped<ISubmissionService, SubmissionService>();

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseStatusCodePages();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();

    if (!context.Submissions.Any())
    {
        context.Submissions.AddRange(
            new Submission
            {
                Content = "Sample submission 1",
                Created = DateTime.UtcNow.AddDays(-2)
            },
            new Submission
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app
    .MapGet("/", () => "Healthy")
    .WithGroupName("Health");
app.MapAllEndpoints();


app.Run();
