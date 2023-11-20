using LibrarySystem.Shared.DTOs;

namespace LibrarySystem.Application.Interfaces
{
    public interface IAuthorService
    {
        Task AddAsync(AuthorDto authorDto);
        Task<AuthorWithBooksDto> GetByIdAsync(int id);
        Task<IEnumerable<AuthorDto>> GetAllAsync(bool includeDeleted);
        Task UpdateAsync(AuthorDto authorDto);
        Task DeleteAsync(int id);
    }
}
