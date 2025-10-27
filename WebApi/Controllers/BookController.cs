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
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks(CancellationToken cancellationToken)
    {
        var books = await _bookService.GetAllBooksAsync(cancellationToken);
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetBookById(int id, CancellationToken cancellationToken)
    {
        var book = await _bookService.GetBookByIdAsync(id, cancellationToken);

        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateBook(CreateBookDTO dto, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var id = await _bookService.AddBookAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetBookById), new { id }, new { Id = id, dto.Title, dto.PublishedYear, dto.AuthorId });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(UpdateBookDTO dto, int id, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var updated = await _bookService.UpdateBookAsync(dto, id,  cancellationToken);

        if (updated)
        {
            return Ok();
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
    {
        var deleted = await _bookService.DeleteBookAsync(id, cancellationToken);

        if (!deleted) {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("publishedAfter/{year:int}")]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooksPublishedAfter(int year, CancellationToken cancellationToken)
    {
        var books = await _bookService.GetBooksPublishedAfterAsync(year, cancellationToken);
        return Ok(books);
    }
}