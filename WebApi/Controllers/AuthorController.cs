using Microsoft.AspNetCore.Mvc;
using Week4Task4pt2.Domain.Models;
using Week4Task4pt2.Application.DTOs;
using Week4Task4pt2.Application.Interfaces;

namespace Week4Task4pt2.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthorController(IAuthorService authorService) : ControllerBase
{
    private readonly IAuthorService _authorService = authorService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors(CancellationToken cancellationToken)
    {
        var authors = await _authorService.GetAllAuthorsAsync(cancellationToken);
        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Author>> GetAuthorById(int id,  CancellationToken cancellationToken)
    {
        var author = await _authorService.GetAuthorByIdAsync(id, cancellationToken);
        
        if (author == null) {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateAuthor(CreateAuthorDTO dto,  CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var id = await _authorService.AddAuthorAsync(dto, cancellationToken);

        return CreatedAtAction(nameof(GetAuthorById), new { id }, new { Id = id, dto.Name, dto.DateOfBirth });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(UpdateAuthorDTO dto, int id,  CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var updated = await _authorService.UpdateAuthorAsync(dto, id,  cancellationToken);

        if (updated) { 
            return Ok();
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id,  CancellationToken cancellationToken)
    {
        var deleted = await _authorService.DeleteAuthorAsync(id,  cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("withBookCount")]
    public async Task<ActionResult<IEnumerable<AuthorBookCountDTO>>> GetAuthorsWithBookCount(CancellationToken cancellationToken)
    {
        var authors = await _authorService.GetAuthorsWithBookCountAsync(cancellationToken);
        return Ok(authors);
    }

    [HttpGet("search/{namePart}")]
    public async Task<ActionResult<IEnumerable<Author>>> FindAuthorsByName(string namePart,  CancellationToken cancellationToken)
    {
        var authors = await _authorService.FindAuthorsByNameAsync(namePart, cancellationToken);
        return Ok(authors);
    }
}