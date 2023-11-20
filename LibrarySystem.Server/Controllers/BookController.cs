using Asp.Versioning;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Server.Controllers
{
    [ApiVersion("1.0")]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] BookDto bookDto)
        {
            await _bookService.AddAsync(bookDto);
            return Ok(bookDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var bookDto = await _bookService.GetByIdAsync(id);
            return Ok(bookDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool includeDeleted = false)
        {
            var books = await _bookService.GetAllAsync(includeDeleted);
            return Ok(books);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookDto bookDto)
        {
            bookDto.Id = id;
            await _bookService.UpdateAsync(bookDto);
            return Ok(bookDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookService.DeleteAsync(id);
            return Ok();
        }
    }
}
