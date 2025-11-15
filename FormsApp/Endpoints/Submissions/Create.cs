using FormsApp.Common;
using FormsApp.Core.Services.Submissions;
using FormsApp.Core.Services.Submissions.Dto;

namespace FormsApp.Endpoints.Submissions;

public class Create : IEndpoint
{
    public void Map(WebApplication app)
    {
        app.MapPost("/api/submissions", async (
                SubmissionDto submission,
                ISubmissionService submissionService,
                CancellationToken ct) =>
            {
                var result = await submissionService.CreateAsync(submission, ct);
                return Results.Created($"/api/submissions/{result.Id}", result);
            })
            .WithName("CreateSubmission")
            .WithTags("Submissions");
    }
}