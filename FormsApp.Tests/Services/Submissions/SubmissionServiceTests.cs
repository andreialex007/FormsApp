using FluentValidation;
using FormsApp.Core.Data;
using FormsApp.Core.Data.Entities;
using FormsApp.Core.Services.Submissions;
using FormsApp.Core.Services.Submissions.Dto;
using Microsoft.EntityFrameworkCore;

namespace FormsApp.Tests.Services.Submissions;

[TestFixture]
public class SubmissionServiceTests
{
    [Test]
    public void SearchAsync_WhenDtoInvalid_ThrowsValidationException()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var invalidRequest = new SubmissionSearchDto(Take: 0);

        Assert.ThrowsAsync<ValidationException>(() => service.SearchAsync(invalidRequest));
    }

    [Test]
    public async Task SearchAsync_WithFilters_ReturnsPagedResults()
    {
        using var context = CreateContext();
        var now = DateTime.UtcNow;
        context.Submissions.AddRange(
            new Submission { Id = 1, Content = "Alpha value", Created = now.AddHours(-2) },
            new Submission { Id = 2, Content = "Beta value", Created = now.AddHours(-1) },
            new Submission { Id = 3, Content = "Another alpha snippet", Created = now }
        );
        await context.SaveChangesAsync();
        var service = CreateService(context);
        var dto = new SubmissionSearchDto(
            Skip: 1,
            Take: 1,
            ContentSearchTerm: "ALPHA",
            DateFrom: now.AddHours(-3),
            DateTo: now.AddMinutes(1)
        );

        var response = await service.SearchAsync(dto);

        Assert.That(response.Total, Is.EqualTo(3));
        Assert.That(response.Found, Is.EqualTo(2));
        Assert.That(response.Items, Has.Count.EqualTo(1));
        Assert.That(response.Items.Single().Id, Is.EqualTo(1));
    }

    [Test]
    public async Task GetByIdAsync_WhenItemExists_ReturnsDto()
    {
        using var context = CreateContext();
        var submission = new Submission { Id = 5, Content = "{}", Created = DateTime.UtcNow };
        context.Submissions.Add(submission);
        await context.SaveChangesAsync();
        var service = CreateService(context);

        var result = await service.GetByIdAsync(5);

        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Id, Is.EqualTo(5));
        Assert.That(result.Content, Is.EqualTo("{}"));
    }

    [Test]
    public async Task GetByIdAsync_WhenItemMissing_ReturnsNull()
    {
        using var context = CreateContext();
        var service = CreateService(context);

        var result = await service.GetByIdAsync(999);

        Assert.That(result, Is.Null);
    }

    [Test]
    public void CreateAsync_WhenJsonInvalid_ThrowsValidationException()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var dto = new SubmissionDto { Content = "not-json" };

        Assert.ThrowsAsync<ValidationException>(() => service.CreateAsync(dto));
    }

    [Test]
    public async Task CreateAsync_WhenDtoValid_PersistsEntityAndReturnsDto()
    {
        using var context = CreateContext();
        var service = CreateService(context);
        var dto = new SubmissionDto
        {
            Content = "{\"name\":\"test\"}",
            Created = DateTime.UtcNow.AddDays(-1)
        };

        var created = await service.CreateAsync(dto);

        Assert.That(created.Id, Is.Not.EqualTo(0));
        Assert.That(created.Content, Is.EqualTo(dto.Content));
        Assert.That(await context.Submissions.CountAsync(), Is.EqualTo(1));
        var entity = await context.Submissions.SingleAsync();
        Assert.That(entity.Content, Is.EqualTo(dto.Content));
        Assert.That(entity.Created, Is.EqualTo(dto.Created));
    }

    [Test]
    public async Task DeleteAsync_WhenItemExists_RemovesAndReturnsTrue()
    {
        using var context = CreateContext();
        context.Submissions.Add(new Submission { Id = 7, Content = "{}", Created = DateTime.UtcNow });
        await context.SaveChangesAsync();
        var service = CreateService(context);

        var removed = await service.DeleteAsync(7);

        Assert.That(removed, Is.True);
        Assert.That(await context.Submissions.AnyAsync(), Is.False);
    }

    [Test]
    public async Task DeleteAsync_WhenItemMissing_ReturnsFalse()
    {
        using var context = CreateContext();
        var service = CreateService(context);

        var removed = await service.DeleteAsync(123);

        Assert.That(removed, Is.False);
    }

    private static SubmissionService CreateService(ApplicationDbContext context) =>
        new(context, new SubmissionSearchRequestValidator(), new SubmissionDtoValidator());

    private static ApplicationDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }
}
