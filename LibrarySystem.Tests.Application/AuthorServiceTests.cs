using FluentValidation;
using LibrarySystem.Application.Services;
using LibrarySystem.Domain.Interfaces;
using LibrarySystem.Domain.Interfaces.Base;
using LibrarySystem.Shared.DTOs;
using AutoMapper;
using Moq;
using LibrarySystem.Domain.Entities;
using FluentValidation.Results;
using LibrarySystem.Shared.Exceptions;

namespace LibrarySystem.Tests.Application
{
    public class AuthorServiceTests
    {
        [Fact]
        public async Task AddAsync_ValidAuthorDto_SuccessfullyAdded()
        {
            // Arrange
            var validAuthorDto = new AuthorDto
            {
                Name = "Test Test T",
            };

            var mockRepository = new Mock<IAuthorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<AuthorDto>>();

            mockValidator.Setup(v => v.Validate(validAuthorDto)).Returns(new FluentValidation.Results.ValidationResult());

            mockRepository.Setup(r => r.ExistsByNameAsync(validAuthorDto.Name)).ReturnsAsync(false);

            var authorService = new AuthorService(
                mockRepository.Object,
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockValidator.Object
            );

            // Act
            await authorService.AddAsync(validAuthorDto);

            // Assert
            mockValidator.Verify(v => v.Validate(validAuthorDto), Times.Once);
            mockRepository.Verify(r => r.ExistsByNameAsync(validAuthorDto.Name), Times.Once);
            mockRepository.Verify(r => r.AddAsync(It.IsAny<Author>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddAsync_InvalidAuthorDto_ThrowsValidationException()
        {
            // Arrange
            var invalidAuthorDto = new AuthorDto
            {
                Name = string.Empty,
            };

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("PropertyName", "Author Name is required.")
            };

            var mockRepository = new Mock<IAuthorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<AuthorDto>>();

            mockValidator.Setup(v => v.Validate(invalidAuthorDto)).Returns(new ValidationResult(validationErrors));

            var authorService = new AuthorService(
                mockRepository.Object,
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockValidator.Object
            );

            // Act and Assert
            await Assert.ThrowsAsync<ValidationException>(() => authorService.AddAsync(invalidAuthorDto));
        }

        [Fact]
        public async Task AddAsync_ExistingAuthorName_ThrowsRepetitionException()
        {
            // Arrange
            var existingAuthorName = "John Doe";

            var authorDto = new AuthorDto
            {
                Name = existingAuthorName,
                // Add other properties as needed
            };

            var mockRepository = new Mock<IAuthorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<AuthorDto>>();

            mockValidator.Setup(v => v.Validate(authorDto)).Returns(new ValidationResult());

            mockRepository.Setup(r => r.ExistsByNameAsync(existingAuthorName)).ReturnsAsync(true);

            var authorService = new AuthorService(
                mockRepository.Object,
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockValidator.Object
            );

            // Act and Assert
            await Assert.ThrowsAsync<RepetitionException>(() => authorService.AddAsync(authorDto));
        }

        [Fact]
        public async Task UpdateAsync_ValidAuthorDtoForExistingAuthor_SuccessfullyUpdated()
        {
            // Arrange
            var existingAuthorId = 1;
            var existingAuthorName = "Test123456";

            var validAuthorDto = new AuthorDto
            {
                Id = existingAuthorId,
                Name = "Updated Name",
                // Add other properties as needed
            };

            var mockRepository = new Mock<IAuthorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<AuthorDto>>();

            mockValidator.Setup(v => v.Validate(validAuthorDto)).Returns(new ValidationResult());

            mockRepository.Setup(r => r.GetByIdAsync(existingAuthorId)).ReturnsAsync(new Author { Id = existingAuthorId, Name = existingAuthorName });
            mockRepository.Setup(r => r.ExistsByNameAsync(validAuthorDto.Name)).ReturnsAsync(false);

            var authorService = new AuthorService(
                mockRepository.Object,
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockValidator.Object
            );

            // Act
            await authorService.UpdateAsync(validAuthorDto);

            // Assert
            mockValidator.Verify(v => v.Validate(validAuthorDto), Times.Once);
            mockRepository.Verify(r => r.GetByIdAsync(existingAuthorId), Times.Once);
            mockRepository.Verify(r => r.ExistsByNameAsync(validAuthorDto.Name), Times.Once);
            mockRepository.Verify(r => r.Update(It.IsAny<Author>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task AddAsync_InvalidAuthorDto_ThrowsRepetitionException()
        {
            // Arrange
            var invalidAuthorDto = new AuthorDto
            {
                Name = "Existing Author Name",
            };

            var validationErrors = new List<ValidationFailure>
            {
                new ValidationFailure("Name", "Author name must be unique.")
            };

            var mockRepository = new Mock<IAuthorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<AuthorDto>>();

            mockValidator.Setup(v => v.Validate(invalidAuthorDto)).Returns(new ValidationResult(validationErrors));

            mockRepository.Setup(r => r.ExistsByNameAsync(invalidAuthorDto.Name)).ReturnsAsync(true);

            var authorService = new AuthorService(
                mockRepository.Object,
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockValidator.Object
            );


            // Act and Assert
            await Assert.ThrowsAsync<ValidationException>(() => authorService.AddAsync(invalidAuthorDto));

        }


        [Fact]
        public async Task DeleteAsync_ValidAuthorId_SuccessfullyDeleted()
        {
            // Arrange
            var validAuthorId = 1;

            var mockRepository = new Mock<IAuthorRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<AuthorDto>>(); // Assuming you have a validator for AuthorDto

            var authorToDelete = new Author
            {
                Id = validAuthorId,
                Name = string.Empty,
            };

            mockRepository.Setup(r => r.GetByIdAsync(validAuthorId)).ReturnsAsync(authorToDelete);

            var authorService = new AuthorService(
                mockRepository.Object,
                mockUnitOfWork.Object,
                mockMapper.Object,
                mockValidator.Object
            );

            // Act
            await authorService.DeleteAsync(validAuthorId);

            // Assert
            mockRepository.Verify(r => r.GetByIdAsync(validAuthorId), Times.Once);
            mockRepository.Verify(r => r.Delete(authorToDelete), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}
