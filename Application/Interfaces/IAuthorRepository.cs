using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.Application.Interfaces;


public interface IAuthorRepository
{
    public Task<IEnumerable<Author>> GetAllAsync();
    public Task<Author?> GetByIdAsync(int id);
    public Task<int> CreateAsync(Author author);
    public Task<bool> UpdateAsync(Author author);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> ExistsAsync(int id);
    public Task<IEnumerable<AuthorBookCountDTO>> GetWithBooksCountAsync();
    public Task<IEnumerable<Author>> FindByNameAsync(string name);
}