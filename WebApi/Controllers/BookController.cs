using Microsoft.AspNetCore.Mvc;
using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.Interfaces;
using Week4Task4pt2.Application.DTOs;

namespace Week4Task4pt2.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController(IBookService bookService) : ControllerBase
{
    private readonly IBookService _bookService = bookService;

    [HttpGet]
    public ActionResult<IEnumerable<Book>> GetAllBooks()
    {
        var books = _bookService.GetAllBooks();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Book> GetBookById(int id)
    {
        Book? book;
        try
        {
            book = _bookService.GetBookById(id);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public ActionResult<int> CreateBook(CreateBookDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int id;
        try
        {
            id = _bookService.AddBook(dto);
        }
        catch (ArgumentException ex) { 
            return BadRequest(ex.Message);
        }

        return CreatedAtAction(nameof(GetBookById), new { id }, new { Id = id, Title = dto.Title, PublishedYear = dto.PublishedYear, AuthorId = dto.AuthorId });
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateBook(UpdateBookDTO dto, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool updated;
        try
        {
            updated = _bookService.UpdateBook(dto, id);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        if (updated)
        {
            return Ok();
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeleteBook(int id)
    {
        var deleted = _bookService.DeleteBook(id);

        if (!deleted) {
            return NotFound();
        }
        return NoContent();
    }
}