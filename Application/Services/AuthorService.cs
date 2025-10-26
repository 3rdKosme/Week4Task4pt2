using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Domain.Exceptions;
using Week4Task4pt2.Application.Interfaces;
using Week4Task4pt2.Application.DTOs;
using Week4Task4pt2.Helpers;

namespace Week4Task4pt2.Application.Services;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<IEnumerable<Author>> GetAllAuthorsAsync()
    {
        return await _authorRepository.GetAllAsync();
    }

    public async Task<Author?> GetAuthorByIdAsync(int id)
    {
        ValidationHelper.CheckId(id);
        return await _authorRepository.GetByIdAsync(id);
    }

    public async Task<int> AddAuthorAsync(CreateAuthorDTO dto)
    {
        CheckDateOfBirth((DateOnly)dto.DateOfBirth);
        var author = new Author
        {
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth
        };
        return await _authorRepository.CreateAsync(author);
    }

    public async Task<bool> UpdateAuthorAsync(UpdateAuthorDTO dto, int id)
    {
        var existingAuthor = await _authorRepository.GetByIdAsync(id) 
            ?? throw new NotFoundException($"Автора с ID = {id} не существует.");

        if (dto.Name is not null)
        {
            existingAuthor.Name = dto.Name;
        }

        if(dto.DateOfBirth is not null)
        {
            CheckDateOfBirth((DateOnly)dto.DateOfBirth);
            existingAuthor.DateOfBirth = (DateOnly)dto.DateOfBirth;
        }

        return await _authorRepository.UpdateAsync(existingAuthor);
    }

    public async Task<bool> DeleteAuthorAsync(int id)
    {
        ValidationHelper.CheckId(id);
        return await _authorRepository.DeleteAsync(id);
    }

    private static void CheckDateOfBirth(DateOnly dateOfBirth)
    {
        var minDate = new DateOnly(0001, 1, 1);

        if(dateOfBirth < minDate || dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ValidationException("Некорректная дата рождения.");
        }
    }
}