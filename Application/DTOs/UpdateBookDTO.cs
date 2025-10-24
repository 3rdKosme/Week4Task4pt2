using System.ComponentModel.DataAnnotations;

namespace Week4Task4pt2.Application.DTOs;

public record UpdateBookDTO
{
    [MinLength(2)]
    [MaxLength(200)]
    public string? Title { get; set; } = null;
    [Range(1, 3000)]
    public int? PublishedYear { get; set; } = null;
    [Range(1, Int32.MaxValue)]
    public int? AuthorId { get; set; } = null;
}