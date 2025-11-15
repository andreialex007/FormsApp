using FormsApp.Common;
using FormsApp.Core.Services.Submissions;
using FormsApp.Core.Services.Submissions.Dto;

namespace FormsApp.Endpoints.Submissions;

public class Search : IEndpoint
{
    public void Map(WebApplication app)
    {
        app.MapPost("/api/submissions/search", async (
            SubmissionSearchDto request,
            ISubmissionService submissionService,
            CancellationToken ct) =>
        {
            var result = await submissionService.SearchAsync(request, ct);
            return Results.Ok(result);
        })
        .WithName("SearchSubmissions")
        .WithTags("Submissions");
    }
}