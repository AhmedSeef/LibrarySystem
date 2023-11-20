using LibrarySystem.Shared.DTOs;

namespace LibrarySystem.Application.Interfaces
{
    public interface IBookService
    {
        Task AddAsync(BookDto bookDto);
        Task<BookDto> GetByIdAsync(int id);
        Task<IEnumerable<BookDto>> GetAllAsync(bool includeDeleted = false);
        Task UpdateAsync(BookDto bookDto);
        Task DeleteAsync(int id);
    }
}
