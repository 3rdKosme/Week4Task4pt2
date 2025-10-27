using Week4Task4pt2.Domain.Models;

namespace Week4Task4pt2.Application.Interfaces;

public interface IBookRepository : IBaseRepository<Book>
{
    public Task<IEnumerable<Book>> GetPublishedAfterAsync(int year, CancellationToken cancellationToken);
}