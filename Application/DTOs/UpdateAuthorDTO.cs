using System.ComponentModel.DataAnnotations;

namespace Week4Task4pt2.Application.DTOs;

public record UpdateAuthorDTO
{
    [MinLength(2)]
    [MaxLength(200)]
    public string? Name { get; set; } = null;
    public DateOnly? DateOfBirth { get; set; } = null;
}