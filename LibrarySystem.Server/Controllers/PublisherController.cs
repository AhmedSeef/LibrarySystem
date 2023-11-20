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
            try
            {
                var publishers = await _publisherService.GetAllAsync(includeDeleted);
                return Ok(publishers);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PublisherDto publisherDto)
        {
            try
            {
                publisherDto.Id = id; // Make sure the ID in the DTO matches the route parameter
                await _publisherService.UpdateAsync(publisherDto);
                return Ok(publisherDto);
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
                await _publisherService.DeleteAsync(id);
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
