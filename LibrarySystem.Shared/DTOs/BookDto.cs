using LibrarySystem.Shared.DTOs.Base;

namespace LibrarySystem.Shared.DTOs
{
    public class BookDto : BaseEntityDto
    { 
        public int AuthorId { get; set; }
        public int PublisherId { get; set; }
    }
}
