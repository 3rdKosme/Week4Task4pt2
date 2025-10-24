using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.Application.Interfaces;

public interface IBookService
{
    public IEnumerable<Book> GetAllBooks();
    public Book? GetBookById(int id);
    public int AddBook(CreateBookDTO dto);
    public bool UpdateBook(UpdateBookDTO dto, int id);
    public bool DeleteBook(int id);
}