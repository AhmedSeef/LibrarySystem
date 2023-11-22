using LibrarySystem.Shared.DTOs;

namespace LibrarySystem.Application.Interfaces
{
    public interface IBookService
    {
        Task AddAsync(BookDto bookDto);
        Task<BookWithAutorsPublishersDto> GetByIdAsync(int id);
        Task<IEnumerable<BookWithAutorsPublishersDto>> GetAllAsync(bool includeDeleted = false);
        Task UpdateAsync(BookDto bookDto);
        Task DeleteAsync(int id);
    }
}
