using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibrarySystem.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
