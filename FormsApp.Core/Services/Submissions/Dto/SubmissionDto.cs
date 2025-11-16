using FluentValidation;
using FormsApp.Core.Common.Models;

namespace FormsApp.Core.Services.Submissions.Dto;

public class SubmissionDto : DtoBase
{
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public string Content { get; set; }
}

public class SubmissionDtoValidator : AbstractValidator<SubmissionDto>
{
    public SubmissionDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty()
            .WithMessage("Content cannot be empty")
            .MaximumLength(1000)
            .WithMessage("Content cannot be longer than 1000 characters");
    }
}