using LibrarySystem.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Domain.Entities
{
    [Table("Author")]
    public class Author : BaseEntity
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }
        public virtual ICollection<Book> Books { get; set; }
    }
}
