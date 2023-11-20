using LibrarySystem.Shared.DTOs.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibrarySystem.Shared.DTOs
{
    public class BookWithAutorsPublishersDto : BaseEntityDto
    {
        public AuthorDto AuthorDto { get; set; }
        public PublisherDto PublisherDto { get; set; }
    }
}
