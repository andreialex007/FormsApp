using FluentValidation;
using FluentValidation.Results;
using FormsApp.Core.Common;
using FormsApp.Core.Common.Models;
using FormsApp.Core.Data;
using FormsApp.Core.Services.Submissions.Dto;
using Microsoft.EntityFrameworkCore;

namespace FormsApp.Core.Services.Submissions;

public class SubmissionService(
    ApplicationDbContext db,
    IValidator<SubmissionSearchDto> submissionSearchValidator,
    IValidator<SubmissionDto> submissionDtoValidator
    ) : ISubmissionService
{
    public async Task<SearchResponse<SubmissionDto>> SearchAsync(SubmissionSearchDto dto, CancellationToken ct = default)
    {
        await submissionSearchValidator.ValidateAndThrowAsync(dto, ct);

        var query = db.Submissions.AsQueryable();
        var total = await db.Submissions.CountAsync(ct);

        if (dto.Id.HasValue) query = query.Where(s => s.Id == dto.Id.Value);
        if (!string.IsNullOrWhiteSpace(dto.ContentSearchTerm)) query = query.Where(s => s.Content.ToLower().Contains(dto.ContentSearchTerm.ToLower()));
        if (dto.DateFrom.HasValue) query = query.Where(s => s.Created >= dto.DateFrom.Value);
        if (dto.DateTo.HasValue) query = query.Where(s => s.Created <= dto.DateTo.Value);

        var found = await query.CountAsync(ct);

        var items = await query
            .OrderByDescending(x=>x.Created)
            .Skip(dto.Skip)
            .Take(dto.Take)
            .ToListAsync(ct);

        return new SearchResponse<SubmissionDto>
        {
            Items = items.Select(x=>x.ToSubmissionDto()).ToList(),
            Found = found,
            Total = total
        };
    }

    public async Task<SubmissionDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var item = await db.Submissions.FindAsync([id], cancellationToken);
        return item?.ToSubmissionDto();
    }

    public async Task<SubmissionDto> CreateAsync(SubmissionDto submission, CancellationToken ct = default)
    {
        await submissionDtoValidator.ValidateAndThrowAsync(submission, ct);

        if(!JsonValidator.IsValidJson(submission.Content))
            throw new ValidationException(new List<ValidationFailure>([ new ValidationFailure(nameof(submission),"Invalid Json") ]));
        
        var entity = submission.ToEntity();
        db.Submissions.Add(entity);
        await db.SaveChangesAsync(ct);
        return entity.ToSubmissionDto();
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var submission = await db.Submissions.FindAsync([id], cancellationToken);
        if (submission == null)
            return false;

        db.Submissions.Remove(submission);
        await db.SaveChangesAsync(cancellationToken);
        return true;
    }
}