using LibrarySystem.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibrarySystem.Domain.Entities
{
    [Table("Publisher")]
    public class Publisher : BaseEntity
    {
        public Publisher()
        {
            Books = new HashSet<Book>();
        }

        public virtual ICollection<Book> Books { get; set; }
    }
}
