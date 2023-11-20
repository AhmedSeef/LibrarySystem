using LibrarySystem.Domain.Interfaces.Base;
using LibrarySystem.Infrastructure.Data;

namespace LibrarySystem.Infrastructure.Repositories.Base
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly DataContext _context;
        private bool disposed = false;

        public UnitOfWork(DataContext context)
        {
            _context = context;
            AuthorRepository = new AuthorRepository(_context);
            PublisherRepository = new PublisherRepository(_context);
            BookRepository = new BookRepository(_context);
        }

        public Domain.Interfaces.IAuthorRepository AuthorRepository { get; }
        public Domain.Interfaces.IPublisherRepository PublisherRepository { get; }
        public Domain.Interfaces.IBookRepository BookRepository { get; }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
