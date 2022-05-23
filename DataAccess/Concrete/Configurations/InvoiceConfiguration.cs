using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.InvoiceNumber).HasMaxLength(50).IsRequired();

            builder.HasMany(x => x.InvoiceDetails).WithOne(x => x.Invoice).HasForeignKey(x => x.InvoiceId);
        }
    }
}
