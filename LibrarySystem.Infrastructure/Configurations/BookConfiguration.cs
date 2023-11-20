using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).IsRequired();

            
            builder.HasOne(b => b.Author)
                .WithMany() 
                .HasForeignKey(b => b.AuthorId)
                .IsRequired();

            
            builder.HasOne(b => b.Publisher)
                .WithMany() 
                .HasForeignKey(b => b.PublisherId)
                .IsRequired();
        }
    }

}
