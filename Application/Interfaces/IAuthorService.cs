using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.Application.Interfaces;

public interface IAuthorService
{
    public Task<IEnumerable<Author>> GetAllAuthorsAsync();
    public Task<Author?> GetAuthorByIdAsync(int id);
    public Task<int> AddAuthorAsync(CreateAuthorDTO dto);
    public Task<bool> UpdateAuthorAsync(UpdateAuthorDTO dto, int id);
    public Task<bool> DeleteAuthorAsync(int id);
}