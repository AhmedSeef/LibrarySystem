using Asp.Versioning;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Server.Controllers
{
    [ApiVersion("1.0")]
    public class BookController : BaseController
    {
        private readonly IBookService _bookService;
        private readonly IPublisherService _publisherService;
        private readonly IAuthorService _authorService;



        public BookController(IBookService bookService, IPublisherService publisherService, IAuthorService authorService)
        {
            _bookService = bookService;
            _publisherService = publisherService;
            _authorService = authorService;
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

        [HttpGet("GetPublishersAndAuthorsLookup")]
        public async Task<IActionResult> GetPublishersAndAuthorsLookup()
        {
            var Publishers = await _publisherService.GetLookupAsync();
            var Authors = await _authorService.GetLookupAsync();

            return Ok(new { Publishers = Publishers, Authors = Authors });
        }
    }
}
