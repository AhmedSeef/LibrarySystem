using AutoMapper;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Interfaces.Base;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Exceptions;

namespace LibrarySystem.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(AuthorDto authorDto)
        {
            if (await _repository.ExistsByNameAsync(authorDto.Name))
            {
                throw new RepetitionException(nameof(authorDto));
            }

            var author = _mapper.Map<Author>(authorDto);
            await _repository.AddAsync(author);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<AuthorWithBooksDto> GetByIdAsync(int id)
        {
            var author = await _repository.GetByIdAsync(id,includes: entity => entity.Books);
            if (author is null)
            {
                throw new NotFoundException(nameof(AuthorWithBooksDto));
            }

            return _mapper.Map<AuthorWithBooksDto>(author);
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAsync(bool includeDeleted)
        {
            var authors = await _repository.GetAllAsync(includeDeleted);
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task UpdateAsync(AuthorDto authorDto)
        {
            var existingAuthor = await _repository.GetByIdAsync(authorDto.Id);
            if (existingAuthor is null)
            {
                throw new NotFoundException(nameof(AuthorDto));
            }

            _mapper.Map(authorDto, existingAuthor);

            _repository.Update(existingAuthor);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var authorToDelete = await _repository.GetByIdAsync(id);
            if (authorToDelete is null)
            {
                throw new NotFoundException(nameof(AuthorDto));
            }

            _repository.Delete(authorToDelete);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
