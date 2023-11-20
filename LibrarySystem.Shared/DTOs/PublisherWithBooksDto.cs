using LibrarySystem.Shared.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
