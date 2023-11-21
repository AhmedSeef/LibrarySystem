using FluentValidation;
using FluentValidation.Results;
using LibrarySystem.Application.Interfaces;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Server.Controllers;
using LibrarySystem.Shared.DTOs;
using LibrarySystem.Shared.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Tests.Controllers
{
    public class AuthorControllerTests
    {
        [Fact]
        public async Task Add_ValidAuthor_ReturnsOkResult()
        {
            // Arrange
            var authorServiceMock = new Mock<IAuthorService>();
            var controller = new AuthorController(authorServiceMock.Object);
            var authorDto = new AuthorDto { Name = "test" };

            // Act
            var result = await controller.Add(authorDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(authorDto, result.Value);
        }

        [Fact]
        public async Task GetById_ExistingAuthor_ReturnsOkResult()
        {
            // Arrange
            var authorServiceMock = new Mock<IAuthorService>();
            var controller = new AuthorController(authorServiceMock.Object);
            var authorId = 1;

            // From Database
            var expectedAuthorDto = new AuthorWithBooksDto
            {
                Id = 1,
                Name = "string2",
                CreatedAt = DateTime.Parse("2023-11-20T09:53:38.99287"),
                EditedAt = DateTime.Parse("2023-11-20T10:33:50.3408597"),
                IsDeleted = false,
                BookDtos = new List<BookDto>
        {
            new BookDto
            {
                AuthorId = 1,
                PublisherId = 1,
                Id = 1,
                Name = "string",
                CreatedAt = DateTime.Parse("2023-11-20T11:10:36.2294178"),
                EditedAt = null,
                IsDeleted = false
            },
            new BookDto
            {
                AuthorId = 1,
                PublisherId = 1,
                Id = 2,
                Name = "string123",
                CreatedAt = DateTime.Parse("2023-11-20T16:55:28.1522416"),
                EditedAt = DateTime.Parse("2023-11-20T14:55:04.943"),
                IsDeleted = true
            }
        }
            };

            authorServiceMock.Setup(x => x.GetByIdAsync(authorId))
                .ReturnsAsync(expectedAuthorDto);

            // Act
            var result = await controller.GetById(authorId) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<AuthorWithBooksDto>(result.Value);
            var authorDto = result.Value as AuthorWithBooksDto;
            Assert.NotNull(authorDto);
            Assert.Equal(authorId, authorDto.Id);
        }

        [Fact]
        public async Task GetById_NonExistingAuthor_ReturnsNotFoundResult()
        {
            // Arrange
            var authorServiceMock = new Mock<IAuthorService>();
            var controller = new AuthorController(authorServiceMock.Object);
            var nonInDatabase = 99; // Assuming this ID does not exist

            authorServiceMock.Setup(x => x.GetByIdAsync(nonInDatabase))
                .ReturnsAsync((AuthorWithBooksDto)null);

            // Act
            var result = await controller.GetById(nonInDatabase) as NotFoundResult;

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Update_ExistingAuthor_ReturnsOkResult()
        {
            // Arrange
            var authorServiceMock = new Mock<IAuthorService>();
            var controller = new AuthorController(authorServiceMock.Object);
            var authorId = 2;

            // Assuming you Author dto with id = 2
            var authorDto = new AuthorDto
            {
                Id = authorId,
                Name = "Updated Author test",
            };

            // Act
            var result = await controller.Update(authorId, authorDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<AuthorDto>(result.Value);
            var updatedAuthorDto = result.Value as AuthorDto;
            Assert.NotNull(updatedAuthorDto);
            Assert.Equal(authorId, updatedAuthorDto.Id);
            Assert.Equal("Updated Author test", updatedAuthorDto.Name);
        }

        [Fact]
        public async Task Delete_ExistingAuthor_ReturnsOkResult()
        {
            // Arrange
            var authorServiceMock = new Mock<IAuthorService>();
            var controller = new AuthorController(authorServiceMock.Object);
            var authorId = 5; // assume this number is in database

            // Act
            var result = await controller.Delete(authorId) as OkResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
        }
    }
}
