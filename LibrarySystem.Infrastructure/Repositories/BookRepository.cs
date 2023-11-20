using LibrarySystem.Domain.Entities;
using LibrarySystem.Infrastructure.Data;
using LibrarySystem.Infrastructure.Repositories.Base;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class BookRepository : GenericRepository<Book>, Domain.Interfaces.IBookRepository
    {
        public BookRepository(DataContext context) : base(context)
        {
        }
    }
}
