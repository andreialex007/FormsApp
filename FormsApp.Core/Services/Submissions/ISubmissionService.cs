using FormsApp.Core.Common.Models;
using FormsApp.Core.Services.Submissions.Dto;

namespace FormsApp.Core.Services.Submissions;

public interface ISubmissionService
{
    Task<SearchResponse<SubmissionDto>> SearchAsync(SubmissionSearchDto dto, CancellationToken ct = default);
    Task<SubmissionDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<SubmissionDto> CreateAsync(SubmissionDto submission, CancellationToken ct = default);
    Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
}