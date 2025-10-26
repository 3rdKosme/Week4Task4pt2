using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Domain.Exceptions;
using Week4Task4pt2.Application.Interfaces;
using Week4Task4pt2.Application.DTOs;
using Week4Task4pt2.Helpers;

namespace Week4Task4pt2.Application.Services;

public class BookService(IBookRepository bookRepository, IAuthorRepository authorRepository) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<IEnumerable<Book>> GetAllBooksAsync()
    {
        return await _bookRepository.GetAllAsync();
    }

    public async Task<Book?> GetBookByIdAsync(int id)
    {
        ValidationHelper.CheckId(id);
        return await _bookRepository.GetByIdAsync(id);
    }

    public async Task<int> AddBookAsync(CreateBookDTO dto)
    {
        CheckYear(dto.PublishedYear);
        await CheckAuthor(dto.AuthorId);

        var book = new Book
        {
            Title = dto.Title,
            PublishedYear = dto.PublishedYear,
            AuthorId = dto.AuthorId
        };
        return await _bookRepository.CreateAsync(book);
    }

    public async Task<bool> UpdateBookAsync(UpdateBookDTO dto, int id)
    {
        var existingBook = await _bookRepository.GetByIdAsync(id) 
            ?? throw new NotFoundException($"Книги с таким Id (id = {id}) не существует.");

        if (dto.Title is not null)
        {
            existingBook.Title = dto.Title;
        }
        if (dto.PublishedYear is not null)
        {
            CheckYear((int)dto.PublishedYear);
            existingBook.PublishedYear = (int)dto.PublishedYear;
        }
        if(dto.AuthorId is not null)
        {
            await CheckAuthor((int)dto.AuthorId);
            existingBook.AuthorId = (int)dto.AuthorId;
        }

        return await _bookRepository.UpdateAsync(existingBook);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        ValidationHelper.CheckId(id);
        return await _bookRepository.DeleteAsync(id);
    }

    private async Task CheckAuthor(int id)
    {
        if(!await _authorRepository.ExistsAsync(id))
        {
            throw new NotFoundException($"Автора с Id = {id} не существует.");
        }
    }

    public async Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year)
    {
        CheckYear(year);
        return await _bookRepository.GetPublishedAfterAsync(year);
    }

    private static void CheckYear(int year)
    {
        if(year > DateTime.UtcNow.Year)
        {
            throw new ValidationException($"Некорректный год публикации.");
        }
    }
}