using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LibrarySystem.Infrastructure.Data
{
    internal class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            string connectionString = "Server=AHMEDSEIF\\SQLEXPRESS;Database=LibrarySystem;TrustServerCertificate=Yes;Trusted_Connection=True;MultipleActiveResultSets=true";
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new DataContext(optionsBuilder.Options);
        }
    }
}
