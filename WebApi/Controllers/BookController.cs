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
    public async Task<ActionResult<IEnumerable<Book>>> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Book>> GetBookById(int id)
    {
        Book? book;
        try
        {
            book = await _bookService.GetBookByIdAsync(id);
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
    public async Task<ActionResult<int>> CreateBook(CreateBookDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int id;
        try
        {
            id = await _bookService.AddBookAsync(dto);
        }
        catch (ArgumentException ex) { 
            return BadRequest(ex.Message);
        }

        return CreatedAtAction(nameof(GetBookById), new { id }, new { Id = id, dto.Title, dto.PublishedYear, dto.AuthorId });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateBook(UpdateBookDTO dto, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool updated;
        try
        {
            updated = await _bookService.UpdateBookAsync(dto, id);
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
    public async Task<IActionResult> DeleteBook(int id)
    {
        var deleted = await _bookService.DeleteBookAsync(id);

        if (!deleted) {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("publishedAfter/{year:int}")]
    public async Task<ActionResult<IEnumerable<Book>>> GetBooksPublishedAfter(int year)
    {
        var books = await _bookService.GetBooksPublishedAfterAsync(year);
        return Ok(books);
    }
}