using Week4Task4pt2.Domain.Models;

namespace Week4Task4pt2.Application.Interfaces;

public interface IBookRepository
{
    public Task<IEnumerable<Book>> GetAllAsync();
    public Task<Book?> GetByIdAsync(int id);
    public Task<int> CreateAsync(Book book);
    public Task<bool> UpdateAsync(Book book);
    public Task<bool> DeleteAsync(int id);
}