namespace Week4Task4pt2.Domain.Models;

public class Author
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public ICollection<Book> Books { get; set; } = null!;
}