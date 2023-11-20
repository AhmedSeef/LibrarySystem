using LibrarySystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibrarySystem.Infrastructure.Configurations
{
    public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();

            builder.HasMany(p => p.Books)
                .WithOne(b => b.Publisher)
                .HasForeignKey(b => b.PublisherId);
        }
    }
}
