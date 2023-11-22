using LibrarySystem.Shared.DTOs.Base;

namespace LibrarySystem.Shared.DTOs
{
    public class AuthorWithBooksDto : BaseEntityDto
    {
        public AuthorWithBooksDto()
        {
            BookDtos = new HashSet<BookDto>();
        }
        public ICollection<BookDto> BookDtos { get; set; }
    }
}
