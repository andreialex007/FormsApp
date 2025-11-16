using FormsApp.Core.Data.Entities;

namespace FormsApp.Core.Services.Submissions.Dto;

public static class SubmissionMapper
{
    public static Submission ToEntity(this SubmissionDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        return new Submission
        {
            Id = dto.Id,
            Created = dto.Created,
            Content = dto.Content
        };
    }

    public static SubmissionDto ToSubmissionDto(this Submission submission)
    {
        ArgumentNullException.ThrowIfNull(submission);

        return new SubmissionDto
        {
            Id = submission.Id,
            Created = submission.Created,
            Content = submission.Content
        };
    }
}
