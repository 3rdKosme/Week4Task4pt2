using System.ComponentModel;
using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Week4Task4pt2.Infrastructure.Persistence.Repositories;

public class BaseRepository<TEntity>(LibraryContext context) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly LibraryContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _dbSet.AsNoTracking().ToListAsync(cancellationToken);
    }

    public virtual async Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

    public virtual async Task<int> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);
        
        var idProperty = typeof(TEntity).GetProperty("Id");
        return idProperty != null ? (int)(idProperty.GetValue(entity) ?? 0) : 0;
    }

    public virtual async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var existing = await _dbSet.FindAsync(new object[] {entity.Id}, cancellationToken);
        if (existing == null) return false;
        
        _context.Entry(existing).CurrentValues.SetValues(entity);
        
        var rowsAffected = await _context.SaveChangesAsync(cancellationToken);
        return rowsAffected > 0;
    }

    public virtual async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _dbSet.FindAsync(new object[] {id}, cancellationToken);
        if(entity == null) return false;
        
        _dbSet.Remove(entity);
        var rowsAffected = await _context.SaveChangesAsync(cancellationToken);
        return rowsAffected > 0;
    }
}