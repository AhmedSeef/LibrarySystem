using AutoMapper;
using LibrarySystem.Domain.Entities;
using LibrarySystem.Shared.DTOs;

namespace LibrarySystem.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<Author, AuthorWithBooksDto>()
          .ForMember(dest => dest.BookDtos, opt => opt.MapFrom(src => src.Books));

            CreateMap<Publisher, PublisherDto>();
            CreateMap<PublisherDto, Publisher>();
            CreateMap<Publisher, PublisherWithBooksDto>()
           .ForMember(dest => dest.BookDtos, opt => opt.MapFrom(src => src.Books));
        }
    }
}
