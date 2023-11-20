using LibrarySystem.Domain.Entities;
using LibrarySystem.Infrastructure.Data;
using LibrarySystem.Infrastructure.Repositories.Base;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, Domain.Interfaces.IAuthorRepository
    {
        public AuthorRepository(DataContext context) : base(context)
        {
        }
    }
}
