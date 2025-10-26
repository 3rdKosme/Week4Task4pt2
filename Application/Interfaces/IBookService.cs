using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.Application.Interfaces;

public interface IBookService
{
    public Task<IEnumerable<Book>> GetAllBooksAsync();
    public Task<Book?> GetBookByIdAsync(int id);
    public Task<int> AddBookAsync(CreateBookDTO dto);
    public Task<bool> UpdateBookAsync(UpdateBookDTO dto, int id);
    public Task<bool> DeleteBookAsync(int id);
    public Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year);
}