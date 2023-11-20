using Asp.Versioning;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Server.Controllers
{
    [ApiVersion("1.0")]
    public class AuthorController : BaseController
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
            var authorDto = await _authorService.GetByIdAsync(id);
            return Ok(authorDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool includeDeleted = false)
        {
            var authors = await _authorService.GetAllAsync(includeDeleted);
            return Ok(authors);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AuthorDto authorDto)
        {
            authorDto.Id = id;
            await _authorService.UpdateAsync(authorDto);
            return Ok(authorDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.DeleteAsync(id);
            return Ok();
        }
    }
}
