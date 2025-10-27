using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Week4Task4pt2.Infrastructure.Persistence.Repositories;

public class BookRepository(LibraryContext context) : BaseRepository<Book>(context), IBookRepository
{
    private readonly LibraryContext _context = context;

    public async Task<IEnumerable<Book>> GetPublishedAfterAsync(int year, CancellationToken cancellationToken = default)
    {
        return await _context.Books.Where(b => b.PublishedYear >= year).ToListAsync(cancellationToken);
    }
}