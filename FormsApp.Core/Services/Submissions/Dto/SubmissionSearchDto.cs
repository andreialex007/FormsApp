using FluentValidation;
using FormsApp.Core.Common.Models;

namespace FormsApp.Core.Services.Submissions.Dto;

public record SubmissionSearchDto(
    int Skip = 0,
    int Take = 10,
    int? Id = null,
    string? ContentSearchTerm = null,
    DateTime? DateFrom = null,
    DateTime? DateTo = null
) : SearchRequest(Skip, Take);


public class SubmissionSearchRequestValidator : AbstractValidator<SubmissionSearchDto>
{
    public SubmissionSearchRequestValidator()
    {
        RuleFor(x => x.Take)
            .GreaterThan(0)
            .WithMessage("Take must be greater than zero");

        RuleFor(x => x.ContentSearchTerm)
            .MaximumLength(200)
            .WithMessage("Content search term cannot be longer than 200 characters");

        RuleFor(x => x.DateFrom)
            .LessThanOrEqualTo(x => x.DateTo)
            .When(x => x.DateFrom.HasValue && x.DateTo.HasValue)
            .WithMessage("DateFrom must be less than or equal to DateTo");
    }
}