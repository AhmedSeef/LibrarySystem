using LibrarySystem.Shared.DTOs.Base;

namespace LibrarySystem.Shared.DTOs
{
    public class PublisherWithBooksDto : BaseEntityDto
    {
        public PublisherWithBooksDto()
        {
            BookDtos = new HashSet<BookDto>();
        }
        public ICollection<BookDto> BookDtos { get; set; }
    }
}
