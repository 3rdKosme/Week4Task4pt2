using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.Interfaces;
using Week4Task4pt2.Helpers;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.Application.Services;

public class BookService(IBookRepository bookRepository, IAuthorRepository authorRepository) : IBookService
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public IEnumerable<Book> GetAllBooks()
    {
        return _bookRepository.GetAll();
    }

    public Book? GetBookById(int id)
    {
        ValidationHelper.CheckId(id);
        return _bookRepository.GetById(id);
    }

    public int AddBook(CreateBookDTO dto)
    {
        CheckAuthor(dto.AuthorId);
        CheckYear(dto.PublishedYear);
        var book = new Book
        {
            Title = dto.Title,
            PublishedYear = dto.PublishedYear,
            AuthorId = dto.AuthorId
        };
        return _bookRepository.Create(book);
    }

    public bool UpdateBook(UpdateBookDTO dto, int id)
    {
        var existingBook = _bookRepository.GetById(id);

        if (existingBook == null)
        {
            throw new ArgumentNullException($"Книги с таким Id (id = {id}) не существует.");
        }

        if(dto.Title is not null)
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
            CheckAuthor((int)dto.AuthorId);
            existingBook.AuthorId = (int)dto.AuthorId;
        }

        return _bookRepository.Update(existingBook);
    }

    public bool DeleteBook(int id)
    {
        ValidationHelper.CheckId(id);
        return _bookRepository.Delete(id);
    }

    private void CheckAuthor(int id)
    {
        if(!_authorRepository.Exists(id))
        {
            throw new ArgumentNullException($"Автора с Id = {id} не существует.");
        }
    }

    private void CheckYear(int year)
    {
        if(year > DateTime.UtcNow.Year)
        {
            throw new ArgumentException($"Некорректный год публикации.");
        }
    }
}