using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.Application.Interfaces;

public interface IAuthorService
{
    public IEnumerable<Author> GetAllAuthors();
    public Author? GetAuthorById(int id);
    public int AddAuthor(CreateAuthorDTO dto);
    public bool UpdateAuthor(UpdateAuthorDTO dto, int id);
    public bool DeleteAuthor(int id);
}