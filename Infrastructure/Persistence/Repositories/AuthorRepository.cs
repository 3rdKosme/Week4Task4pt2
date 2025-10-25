using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Week4Task4pt2.Infrastructure.Persistence.Repositories;

public class AuthorRepository(LibraryContext context) : IAuthorRepository
{
    private readonly LibraryContext _context = context;

    public async Task<IEnumerable<Author>> GetAllAsync()
    {
        return await _context.Authors.AsNoTracking().ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(int id)
    {
        return await _context.Authors.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> CreateAsync(Author author)
    {
        var entity = _context.Authors.Add(author).Entity;
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Author author)
    {
        var existing = await _context.Authors.FindAsync(author.Id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(author);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rowsAffected = await _context.Authors.Where(x => x.Id == id).ExecuteDeleteAsync();
        return rowsAffected > 0;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Authors.AnyAsync(x => x.Id == id);
    }
}