namespace Week4Task4pt2.Domain.Models;

public class Book :  BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public int PublishedYear { get; set; }
    public int AuthorId { get; set; }
    public Author Author { get; set; } = null!;
}