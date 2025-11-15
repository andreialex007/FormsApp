using FormsApp.Common;
using FormsApp.Core.Models;
using FormsApp.Core.Services.Submission;
using FormsApp.Core.Services.Submission.Dto;

namespace FormsApp.Endpoints.Submissions;

public class Search : IEndpoint
{
    public void Map(WebApplication app)
    {
        app.MapPost("/api/submissions/search", async (
            SubmissionSearchRequest request,
            SubmissionService submissionService,
            CancellationToken ct) =>
        {
            var result = await submissionService.SearchAsync(request, ct);
            return Results.Ok(result);
        })
        .WithName("SearchSubmissions")
        .WithOpenApi();
    }
}