namespace FormsApp.Core.Common.Models;

public record SearchResponse<T>
{
    public required List<T> Items { get; init; }
    public int Returned => Items.Count;
    public required int Found { get; init; }
    public required int Total { get; init; }
}