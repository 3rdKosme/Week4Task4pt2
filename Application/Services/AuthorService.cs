using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Domain.Exceptions;
using Week4Task4pt2.Application.Interfaces;
using Week4Task4pt2.Application.DTOs;
using Week4Task4pt2.Application.Constants;
using Week4Task4pt2.Helpers;

namespace Week4Task4pt2.Application.Services;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync(CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Author?> GetAuthorByIdAsync(int id, CancellationToken cancellationToken)
    {
        ValidationHelper.CheckId(id);
        return await _authorRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<int> AddAuthorAsync(CreateAuthorDTO dto, CancellationToken cancellationToken)
    {
        CheckDateOfBirth((DateOnly)dto.DateOfBirth);
        var author = new Author
        {
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth
        };
        return await _authorRepository.CreateAsync(author, cancellationToken);
    }

    public async Task<bool> UpdateAuthorAsync(UpdateAuthorDTO dto, int id, CancellationToken cancellationToken)
    {
        var existingAuthor = await _authorRepository.GetByIdAsync(id, cancellationToken) 
            ?? throw new NotFoundException(ErrorMessages.Authors.NotFound);

        if (dto.Name is not null)
        {
            existingAuthor.Name = dto.Name;
        }

        if(dto.DateOfBirth is not null)
        {
            CheckDateOfBirth((DateOnly)dto.DateOfBirth);
            existingAuthor.DateOfBirth = (DateOnly)dto.DateOfBirth;
        }

        return await _authorRepository.UpdateAsync(existingAuthor, cancellationToken);
    }

    public async Task<bool> DeleteAuthorAsync(int id, CancellationToken cancellationToken)
    {
        ValidationHelper.CheckId(id);
        return await _authorRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<IEnumerable<AuthorBookCountDTO>> GetAuthorsWithBookCountAsync(CancellationToken cancellationToken)
    {
        return await _authorRepository.GetWithBooksCountAsync(cancellationToken);
    }

    public async Task<IEnumerable<Author>> FindAuthorsByNameAsync(string namePart, CancellationToken cancellationToken)
    {
        return await _authorRepository.FindByNameAsync(namePart.Trim(), cancellationToken);
    }

    private static void CheckDateOfBirth(DateOnly dateOfBirth)
    {
        var minDate = new DateOnly(0001, 1, 1);

        if(dateOfBirth < minDate || dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ValidationException(ErrorMessages.Authors.IncorrectBirthDate);
        }
    }
}