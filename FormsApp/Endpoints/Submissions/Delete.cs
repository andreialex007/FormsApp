using FormsApp.Common;
using FormsApp.Core.Services.Submissions;

namespace FormsApp.Endpoints.Submissions;

public class Delete : IEndpoint
{
    public void Map(WebApplication app)
    {
        app.MapDelete("/api/submissions/{id:int}", async (
            int id,
            ISubmissionService submissionService,
            CancellationToken ct) =>
        {
            var deleted = await submissionService.DeleteAsync(id, ct);
            return deleted ? Results.NoContent() : Results.NotFound();
        })
        .WithName("DeleteSubmission")
        .WithTags("Submissions");
    }
}