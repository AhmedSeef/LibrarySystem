using LibrarySystem.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Domain.Entities
{
    [Table("Book")]
    public class Book : BaseEntity
    {
            
        public int AuthorId { get; set; }
        public virtual Author? Author { get; set; }

        public int PublisherId { get; set; }
        public virtual Publisher? Publisher { get; set; }
    }
}
