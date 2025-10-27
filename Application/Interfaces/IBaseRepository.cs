namespace Week4Task4pt2.Application.Interfaces;

public interface IBaseRepository<TEntity> where TEntity : class
{
    public Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
    public Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<int> CreateAsync(TEntity entity, CancellationToken cancellationToken);
    public Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    public Task<bool> DeleteAsync(int id, CancellationToken cancellationToken);
}