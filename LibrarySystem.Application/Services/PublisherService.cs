using AutoMapper;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Interfaces.Base;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Exceptions;

namespace LibrarySystem.Application.Services
{
    public class PublisherService : IPublisherService
    {
        private readonly IPublisherRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PublisherService(IPublisherRepository repository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(PublisherDto publisherDto)
        {
            if (await _repository.ExistsByNameAsync(publisherDto.Name))
            {
                throw new RepetitionException(nameof(publisherDto));
            }

            var publisher = _mapper.Map<Publisher>(publisherDto);

            await _repository.AddAsync(publisher);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PublisherWithBooksDto> GetByIdAsync(int id)
        {
            var publisher = await _repository.GetByIdAsync(id, includes: entity => entity.Books);
            if (publisher is null)
            {
                throw new NotFoundException(nameof(PublisherWithBooksDto));
            }

            return _mapper.Map<PublisherWithBooksDto>(publisher);
        }

        public async Task<IEnumerable<PublisherDto>> GetAllAsync(bool includeDeleted)
        {
            var publishers = await _repository.GetAllAsync(includeDeleted);
            return _mapper.Map<IEnumerable<PublisherDto>>(publishers);
        }

        public async Task UpdateAsync(PublisherDto publisherDto)
        {
            var existingPublisher = await _repository.GetByIdAsync(publisherDto.Id);
            if (existingPublisher is null)
            {
                throw new NotFoundException(nameof(PublisherDto));
            }

            _mapper.Map(publisherDto, existingPublisher);

            _repository.Update(existingPublisher);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var publisherToDelete = await _repository.GetByIdAsync(id);
            if (publisherToDelete is null)
            {
                throw new NotFoundException(nameof(PublisherDto));
            }

            _repository.Delete(publisherToDelete);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
