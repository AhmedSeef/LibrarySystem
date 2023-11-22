using LibrarySystem.Shared.DTOs;

namespace LibrarySystem.Application.Interfaces
{
    public interface IPublisherService
    {
        Task AddAsync(PublisherDto publisherDto);
        Task<PublisherWithBooksDto> GetByIdAsync(int id);
        Task<IEnumerable<LookupItemDto>> GetLookupAsync();
        Task<IEnumerable<PublisherDto>> GetAllAsync(bool includeDeleted = false);
        Task UpdateAsync(PublisherDto publisherDto);
        Task DeleteAsync(int id);
    }
}
