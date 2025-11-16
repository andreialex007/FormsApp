using System.Reflection;
using FluentValidation;
using FormsApp.Common;
using FormsApp.Core.Data;
using FormsApp.Core.Services.Submissions;
using FormsApp.Core.Services.Submissions.Dto;
using FormsApp.Data;
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
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
app.UseStatusCodePages();
app.UseCors();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.EnsureCreated();
    DatabaseSeeder.Seed(context);
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
