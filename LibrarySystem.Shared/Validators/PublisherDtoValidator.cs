using FluentValidation;
using LibrarySystem.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Shared.Validators
{
    public class PublisherDtoValidator : AbstractValidator<PublisherDto>
    {
        public PublisherDtoValidator()
        {
            RuleFor(publisherDto => publisherDto.Name).NotEmpty().WithMessage("Name is required.")
            .Length(10, 50).WithMessage("Name must be between 10 and 50 characters.");
        }
    }
}
