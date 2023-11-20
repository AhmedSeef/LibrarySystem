using LibrarySystem.Application.Interfaces;
using LibrarySystem.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] PublisherDto publisherDto)
        {
            await _publisherService.AddAsync(publisherDto);
            return Ok(publisherDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var publisherWithBooksDto = await _publisherService.GetByIdAsync(id);
            return Ok(publisherWithBooksDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(bool includeDeleted = false)
        {
            var publishers = await _publisherService.GetAllAsync(includeDeleted);
            return Ok(publishers);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PublisherDto publisherDto)
        {
            publisherDto.Id = id;
            await _publisherService.UpdateAsync(publisherDto);
            return Ok(publisherDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _publisherService.DeleteAsync(id);
            return Ok();
        }
    }
}
