using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.Application.Interfaces;

public interface IBookService
{
    public Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken);
    public Task<Book?> GetBookByIdAsync(int id, CancellationToken cancellationToken);
    public Task<int> AddBookAsync(CreateBookDTO dto, CancellationToken cancellationToken);
    public Task<bool> UpdateBookAsync(UpdateBookDTO dto, int id, CancellationToken cancellationToken);
    public Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year, CancellationToken cancellationToken);
}