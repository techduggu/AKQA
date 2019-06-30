using AKQA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AKQA.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(e => e.CustomerId)
                .IsRequired()
                .HasColumnType("uniqueidentifier")
                .ValueGeneratedNever();

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(e => e.MiddleName)
                .HasMaxLength(60);

            builder.Property(e => e.LastName)
                .HasMaxLength(60);

            builder.Property(e => e.Amount)
                .HasColumnType("money");
        }
    }
}
