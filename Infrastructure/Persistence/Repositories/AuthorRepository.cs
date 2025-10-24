using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.Interfaces;

namespace Week4Task4pt2.Infrastructure.Persistence.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private static readonly List<Author> _authors = new List<Author>();
    private static int _nextId = 1;

    public IEnumerable<Author> GetAll()
    {
        return _authors.ToList().AsReadOnly();
    }

    public Author? GetById(int id)
    {
        var user = _authors.FirstOrDefault(x => x.Id == id);
        return user;
    }

    public int Create(Author author)
    {
        author.Id = _nextId++;
        _authors.Add(author);
        return author.Id;
    }

    public bool Update(Author author)
    {
        int id = _authors.FindIndex(x => x.Id == author.Id);
        if (id != -1) { 
            _authors[id] = author;
            return true;
        }
        return false;
    }

    public bool Delete(int id)
    {
        var removed = _authors.RemoveAll(x => x.Id == id) > 0;
        return removed;
    }

    public bool Exists(int id)
    {
        return _authors.Any(x => x.Id == id);
    }
}