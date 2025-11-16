namespace FormsApp.Core.Data.Entities;

public class Submission : EntityBase
{
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public string Content { get; set; }
}