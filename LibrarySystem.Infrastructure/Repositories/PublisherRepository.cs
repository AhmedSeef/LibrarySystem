using LibrarySystem.Domain.Entities;
using LibrarySystem.Infrastructure.Data;
using LibrarySystem.Infrastructure.Repositories.Base;

namespace LibrarySystem.Infrastructure.Repositories
{
    public class PublisherRepository : GenericRepository<Publisher>, Domain.Interfaces.IPublisherRepository
    {
        public PublisherRepository(DataContext context) : base(context)
        {
        }
    }
}
