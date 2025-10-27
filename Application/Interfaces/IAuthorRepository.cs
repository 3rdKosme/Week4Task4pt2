using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.Application.Interfaces;


public interface IAuthorRepository : IBaseRepository<Author>
{
    public Task<bool> ExistsAsync(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<AuthorBookCountDTO>> GetWithBooksCountAsync(CancellationToken cancellationToken);
    public Task<IEnumerable<Author>> FindByNameAsync(string name, CancellationToken cancellationToken);
}