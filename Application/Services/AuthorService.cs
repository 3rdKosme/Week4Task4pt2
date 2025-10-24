using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.Interfaces;
using Week4Task4pt2.Application.DTOs;
using Week4Task4pt2.Helpers;

namespace Week4Task4pt2.Application.Services;

public class AuthorService(IAuthorRepository authorRepository) : IAuthorService
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public IEnumerable<Author> GetAllAuthors()
    {
        return _authorRepository.GetAll();
    }

    public Author? GetAuthorById(int id)
    {
        ValidationHelper.CheckId(id);
        return _authorRepository.GetById(id);
    }

    public int AddAuthor(CreateAuthorDTO dto)
    {
        CheckDateOfBirth((DateOnly)dto.DateOfBirth);
        var author = new Author
        {
            Name = dto.Name,
            DateOfBirth = dto.DateOfBirth
        };
        return _authorRepository.Create(author);
    }

    public bool UpdateAuthor(UpdateAuthorDTO dto, int id)
    {
        var existingAuthor = _authorRepository.GetById(id);

        if (existingAuthor == null) {
            throw new ArgumentNullException($"Автора с ID = {id} не существует.");
        }

        if(dto.Name is not null)
        {
            existingAuthor.Name = dto.Name;
        }

        if(dto.DateOfBirth is not null)
        {
            CheckDateOfBirth((DateOnly)dto.DateOfBirth);
            existingAuthor.DateOfBirth = (DateOnly)dto.DateOfBirth;
        }

        return _authorRepository.Update(existingAuthor);
    }

    public bool DeleteAuthor(int id)
    {
        ValidationHelper.CheckId(id);
        return _authorRepository.Delete(id);
    }

    private void CheckDateOfBirth(DateOnly dateOfBirth)
    {
        var minDate = new DateOnly(0001, 1, 1);

        if(dateOfBirth < minDate || dateOfBirth > DateOnly.FromDateTime(DateTime.Now))
        {
            throw new ArgumentException("Некорректная дата рождения.");
        }
    }
}