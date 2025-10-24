using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Week4Task4pt2.Infrastructure.Persistence.Repositories;

public class BookRepository : IBookRepository
{
    private static readonly List<Book> _books = new List<Book>();
    private static int _nextId = 1;

    public IEnumerable<Book> GetAll()
    {
        return _books.ToList().AsReadOnly();
    }

    public Book? GetById(int id)
    {
        var book = _books.FirstOrDefault(x => x.Id == id);
        return book;
    }

    public int Create(Book book)
    {
        book.Id = _nextId++;
        _books.Add(book);
        return book.Id;
    }

    public bool Update(Book book)
    {
        int id = _books.FindIndex(x => x.Id == book.Id);
        if (id != -1)
        {
            _books[id] = book;
            return true;
        }
        return false;
    }

    public bool Delete(int id)
    {
        var removed = _books.RemoveAll(x => x.Id == id) > 0;
        return removed;
    }
}