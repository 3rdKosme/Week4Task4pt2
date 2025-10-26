namespace Week4Task4pt2.Application.DTOs;

public record AuthorBookCountDTO
{
    public string Name { get; set; } = null!;
    public int BookCount { get; set; }
}
