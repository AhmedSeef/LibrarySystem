using LibrarySystem.Application.Interfaces;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookDto bookDto)
        {
            try
            {
                await _bookService.AddAsync(bookDto);
                return Ok(bookDto);
            }
            catch (RepetitionException ex)
            {
                return Conflict($"Book with the same title, author, and publisher already exists");
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var bookDto = await _bookService.GetByIdAsync(id);
                if (bookDto != null)
                {
                    return Ok(bookDto);
                }
                else
                {
                    return NotFound($"Book with ID {id} not found");
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
                var books = await _bookService.GetAllAsync(includeDeleted);
                return Ok(books);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDto bookDto)
        {
            try
            {
                bookDto.Id = id; // Make sure the ID in the DTO matches the route parameter
                await _bookService.UpdateAsync(bookDto);
                return Ok(bookDto);
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
                await _bookService.DeleteAsync(id);
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
