using System.Data.Entity.ModelConfiguration;
using AAC.Business.Models.Products;

namespace AAC.Infra.Data.Context.Mappings
{
    public class ProductConfig : EntityTypeConfiguration<Product>
    {
        public ProductConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            Property(p => p.Description)
                .IsRequired()
                .HasMaxLength(1000);

            Property(p => p.Image)
                .IsRequired()
                .HasMaxLength(100);

            HasRequired(p => p.Provider)
                .WithMany(p => p.Products)
                .HasForeignKey(p => p.ProviderId);

            ToTable("Products");
        }
    }
}