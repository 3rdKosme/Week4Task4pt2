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
    public async Task<ActionResult<IEnumerable<Author>>> GetAllAuthors()
    {
        var authors = await _authorService.GetAllAuthorsAsync();
        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Author>> GetAuthorById(int id)
    {
        Author? author;
        try
        {
            author = await _authorService.GetAuthorByIdAsync(id);
        } catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        
        if (author == null) {
            return NotFound();
        }
        return Ok(author);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateAuthor(CreateAuthorDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int id;
        try
        {
            id = await _authorService.AddAuthorAsync(dto);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        return CreatedAtAction(nameof(GetAuthorById), new { id }, new { Id = id, dto.Name, dto.DateOfBirth });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAuthor(UpdateAuthorDTO dto, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool updated;
        try
        {
            updated = await _authorService.UpdateAuthorAsync(dto, id);
        }
        catch (ArgumentException ex) { 
            return BadRequest(ex.Message);
        }
        if (updated) { 
            return Ok();
        }
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAuthor(int id)
    {
        var deleted = await _authorService.DeleteAuthorAsync(id);

        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}