using LibrarySystem.Application.Interfaces;
using LibrarySystem.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] AuthorDto authorDto)
        { 
            await _authorService.AddAsync(authorDto);
            return Ok(authorDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var authorDto = await _authorService.GetByIdAsync(id);
                if (authorDto != null)
                {
                    return Ok(authorDto);
                }
                else
                {
                    return NotFound($"Author with ID {id} not found");
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool includeDeleted = false)
        {
            try
            {
                var authors = await _authorService.GetAllAsync(includeDeleted);
                return Ok(authors);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorDto authorDto)
        {
            try
            {
                authorDto.Id = id; // Make sure the ID in the DTO matches the route parameter
                await _authorService.UpdateAsync(authorDto);
                return Ok(authorDto);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _authorService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }
}
