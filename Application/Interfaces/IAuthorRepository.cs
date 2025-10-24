using Week4Task4pt2.Domain.Models;

namespace Week4Task4pt2.Application.Interfaces;


public interface IAuthorRepository
{
    public IEnumerable<Author> GetAll();
    public Author? GetById(int id);
    public int Create(Author author);
    public bool Update(Author author);
    public bool Delete(int id);
    public bool Exists(int id);
}