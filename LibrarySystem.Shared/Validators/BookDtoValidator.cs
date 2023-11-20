using FluentValidation;
using LibrarySystem.Shared.DTOs;

namespace LibrarySystem.Shared.Validators
{
    public class BookDtoValidator : AbstractValidator<BookDto>
    {
        public BookDtoValidator()
        {
            RuleFor(bookDto => bookDto.Name).NotEmpty().WithMessage("Name is required.")
            .Length(10, 50).WithMessage("Name must be between 10 and 50 characters.");
        }
    }
}
