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
    public ActionResult<IEnumerable<Author>> GetAllAuthors()
    {
        var authors = _authorService.GetAllAuthors();
        return Ok(authors);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Author> GetAuthorById(int id)
    {
        Author? author;
        try
        {
            author = _authorService.GetAuthorById(id);
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
    public ActionResult<int> CreateAuthor(CreateAuthorDTO dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        int id;
        try
        {
            id = _authorService.AddAuthor(dto);
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        return CreatedAtAction(nameof(GetAuthorById), new { id }, new { Id = id, Name = dto.Name, DateOfBirth = dto.DateOfBirth });
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAuthor(UpdateAuthorDTO dto, int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        bool updated;
        try
        {
            updated = _authorService.UpdateAuthor(dto, id);
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
    public IActionResult DeleteAuthor(int id)
    {
        var deleted = _authorService.DeleteAuthor(id);

        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}