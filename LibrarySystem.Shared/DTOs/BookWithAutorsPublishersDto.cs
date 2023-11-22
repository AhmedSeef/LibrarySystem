using LibrarySystem.Shared.DTOs.Base;

namespace LibrarySystem.Shared.DTOs
{
    public class BookWithAutorsPublishersDto : BaseEntityDto
    {
        public AuthorDto AuthorDto { get; set; }
        public PublisherDto PublisherDto { get; set; }
    }
}
