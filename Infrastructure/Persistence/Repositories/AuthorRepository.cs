using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.Infrastructure.Persistence.Repositories;

public class AuthorRepository(LibraryContext context) : BaseRepository<Author>(context), IAuthorRepository
{
    private readonly LibraryContext _context = context;
    
    public async Task<bool> ExistsAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Authors.AnyAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<AuthorBookCountDTO>> GetWithBooksCountAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Authors
            .Select(a => new AuthorBookCountDTO 
            {
                Name = a.Name,
                BookCount = a.Books.Count()
            }).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Author>> FindByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Authors.Where(a => a.Name.Contains(name)).ToListAsync(cancellationToken);
    }
}