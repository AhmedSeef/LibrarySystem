using AutoMapper;
using FluentValidation;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Interfaces.Base;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Exceptions;

namespace LibrarySystem.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IValidator<BookDto> _bookDtoValidator;

        public BookService(
            IBookRepository repository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IValidator<BookDto> bookDtoValidator)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bookDtoValidator = bookDtoValidator;
        }

        public async Task AddAsync(BookDto bookDto)
        {
            var validationResult = _bookDtoValidator.Validate(bookDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            if (await _repository.ExistsByNameAsync(bookDto.Name))
            {
                throw new RepetitionException(nameof(bookDto));
            }

            var book = _mapper.Map<Book>(bookDto);

            await _repository.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<BookWithAutorsPublishersDto> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id,entity => entity.Author,entity => entity.Publisher);
            if (book is null)
            {
                throw new NotFoundException(nameof(BookWithAutorsPublishersDto));
            }

            return _mapper.Map<BookWithAutorsPublishersDto>(book);
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync(bool includeDeleted)
        {
            var books = await _repository.GetAllAsync(includeDeleted);
            return _mapper.Map<IEnumerable<BookDto>>(books);
        }

        public async Task UpdateAsync(BookDto bookDto)
        {
            var validationResult = _bookDtoValidator.Validate(bookDto);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var existingBook = await _repository.GetByIdAsync(bookDto.Id);
            if (existingBook is null)
            {
                throw new NotFoundException(nameof(BookDto));
            }

            if (await _repository.ExistsByNameAsync(bookDto.Name))
            {
                throw new RepetitionException(nameof(bookDto));
            }

            _mapper.Map(bookDto, existingBook);

            _repository.Update(existingBook);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var bookToDelete = await _repository.GetByIdAsync(id);
            if (bookToDelete is null)
            {
                throw new NotFoundException(nameof(BookDto));
            }

            _repository.Delete(bookToDelete);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
