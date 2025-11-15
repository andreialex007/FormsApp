using FormsApp.Common;
using FormsApp.Core.Services.Submissions;

namespace FormsApp.Endpoints.Submissions;

public class Get : IEndpoint
{
    public void Map(WebApplication app)
    {
        app.MapGet("/api/submissions/{id:int}", async (
            int id,
            ISubmissionService submissionService,
            CancellationToken ct) =>
        {
            var result = await submissionService.GetByIdAsync(id, ct);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetSubmission")
        .WithTags("Submissions");
    }
}
