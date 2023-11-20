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
            CreateMap<Book, BookWithAutorsPublishersDto>()
                .ForMember(dest => dest.AuthorDto, opt => opt.MapFrom(src => src.Author))
                .ForMember(dest => dest.PublisherDto, opt => opt.MapFrom(src => src.Publisher));

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
