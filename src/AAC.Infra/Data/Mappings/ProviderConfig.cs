using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using AAC.Business.Models.Providers;

namespace AAC.Infra.Data.Mappings
{
    public class ProviderConfig : EntityTypeConfiguration<Provider>
    {
        public ProviderConfig()
        {
            HasKey(p => p.Id);

            Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(200);

            Property(p => p.Document)
                .HasMaxLength(9)
                .HasColumnAnnotation("IX_Document",
                    new IndexAnnotation(new IndexAttribute { IsUnique = true }))
                .IsFixedLength();

            HasRequired(p => p.Address)
                .WithRequiredPrincipal(a => a.Provider);

            ToTable("Providers");
        }
    }
}
