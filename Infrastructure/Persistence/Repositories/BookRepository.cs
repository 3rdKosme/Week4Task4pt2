using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Week4Task4pt2.Infrastructure.Persistence.Repositories;

public class BookRepository(LibraryContext context) : IBookRepository
{
    private readonly LibraryContext _context = context;

    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _context.Books.AsNoTracking().ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(int id)
    {
        return await _context.Books.AsNoTracking().FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<int> CreateAsync(Book book)
    {
        var entity = _context.Books.Add(book).Entity;
        await _context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task<bool> UpdateAsync(Book book)
    {
        var existing = await _context.Books.FindAsync(book.Id);
        if (existing == null) return false;

        _context.Entry(existing).CurrentValues.SetValues(book);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var rowsAffected = await _context.Books.Where(b => b.Id == id).ExecuteDeleteAsync();
        return rowsAffected > 0;
    }
}