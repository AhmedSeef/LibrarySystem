using FluentValidation;
using LibrarySystem.Shared.DTOs;

namespace LibrarySystem.Shared.Validators
{
    public class AuthorDtoValidator : AbstractValidator<AuthorDto>
    {
        public AuthorDtoValidator()
        {
            RuleFor(authorDto => authorDto.Name).NotEmpty().WithMessage("Name is required.")
            .Length(10, 50).WithMessage("Name must be between 10 and 50 characters.");
        }
    }
}
