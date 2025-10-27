using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.Application.Interfaces;

public interface IAuthorService
{
    public Task<IEnumerable<Author>> GetAllAuthorsAsync(CancellationToken cancellationToken);
    public Task<Author?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken);
    public Task<int> AddAuthorAsync(CreateAuthorDTO dto, CancellationToken cancellationToken);
    public Task<bool> UpdateAuthorAsync(UpdateAuthorDTO dto, int id, CancellationToken cancellationToken);
    public Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken);
    public Task<IEnumerable<AuthorBookCountDTO>> GetAuthorsWithBookCountAsync(CancellationToken cancellationToken);
    public Task<IEnumerable<Author>> FindAuthorsByNameAsync(string name, CancellationToken cancellationToken);
}