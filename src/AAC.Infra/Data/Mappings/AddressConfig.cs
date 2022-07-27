using System.Data.Entity.ModelConfiguration;
using AAC.Business.Models.Providers;

namespace AAC.Infra.Data.Mappings
{
    public class AddressConfig: EntityTypeConfiguration<Address>
    {
        public AddressConfig()
        {
            HasKey(a => a.Id);

            Property(a => a.HouseNumber)
                .IsRequired()
                .HasMaxLength(50);

            Property(a => a.Street)
                .IsRequired()
                .HasMaxLength(200);

            Property(a => a.AptNumber)
                .HasMaxLength(50)
                .IsFixedLength();

            Property(a => a.City)
                .IsRequired()
                .HasMaxLength(200);

            Property(a => a.State)
                .IsRequired()
                .HasMaxLength(100);

            Property(a => a.PostalCode)
                .IsRequired()
                .HasMaxLength(10)
                .IsFixedLength();

            ToTable("Addresses");
        }
    }
}
