using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Domain.Exceptions;
using Week4Task4pt2.Application.Interfaces;
using Week4Task4pt2.Application.DTOs;
using Week4Task4pt2.Application.Constants;
using Week4Task4pt2.Helpers;

namespace Week4Task4pt2.Application.Services;

public class BookService(IBookRepository bookRepository, IAuthorRepository authorRepository) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<IEnumerable<Book>> GetAllBooksAsync(CancellationToken cancellationToken =  default)
    {
        return await _bookRepository.GetAllAsync(cancellationToken);
    }

    public async Task<Book?> GetBookByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        ValidationHelper.CheckId(id);
        return await _bookRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<int> AddBookAsync(CreateBookDTO dto, CancellationToken cancellationToken = default)
    {
        CheckYear(dto.PublishedYear);
        await CheckAuthor(dto.AuthorId, cancellationToken);

        var book = new Book
        {
            Title = dto.Title,
            PublishedYear = dto.PublishedYear,
            AuthorId = dto.AuthorId
        };
        return await _bookRepository.CreateAsync(book, cancellationToken);
    }

    public async Task<bool> UpdateBookAsync(UpdateBookDTO dto, int id, CancellationToken cancellationToken = default)
    {
        var existingBook = await _bookRepository.GetByIdAsync(id, cancellationToken) 
            ?? throw new NotFoundException(ErrorMessages.Books.NotFound);

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
            await CheckAuthor((int)dto.AuthorId, cancellationToken);
            existingBook.AuthorId = (int)dto.AuthorId;
        }

        return await _bookRepository.UpdateAsync(existingBook, cancellationToken);
    }

    public async Task<bool> DeleteBookAsync(int id, CancellationToken cancellationToken = default)
    {
        ValidationHelper.CheckId(id);
        return await _bookRepository.DeleteAsync(id, cancellationToken);
    }

    private async Task CheckAuthor(int id, CancellationToken cancellationToken = default)
    {
        if(!await _authorRepository.ExistsAsync(id, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Authors.NotFound);
        }
    }

    public async Task<IEnumerable<Book>> GetBooksPublishedAfterAsync(int year, CancellationToken cancellationToken = default)
    {
        CheckYear(year);
        return await _bookRepository.GetPublishedAfterAsync(year, cancellationToken);
    }

    private static void CheckYear(int year)
    {
        if(year > DateTime.UtcNow.Year)
        {
            throw new ValidationException(ErrorMessages.Books.IncorrectPublicationDate);
        }
    }
}