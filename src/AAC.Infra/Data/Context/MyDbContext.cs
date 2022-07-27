using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AAC.Business.Models.Products;
using AAC.Business.Models.Providers;
using AAC.Infra.Data.Mappings;

namespace AAC.Infra.Data.Context
{
    public class MyDbContext : DbContext
    {
        public MyDbContext()
            : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingEntitySetNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>()
                .Configure(p => p
                    .HasColumnType("varchar")
                    .HasMaxLength(100));

            modelBuilder.Configurations.Add(new ProviderConfig());
            modelBuilder.Configurations.Add(new AddressConfig());
            modelBuilder.Configurations.Add(new ProductConfig());

            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            const string date = "RegistrationDate";

            foreach (var entry in ChangeTracker.Entries()
                         .Where(e => e.Entity.GetType().GetProperty(date) != null))
            {
                if (entry.State == EntityState.Added)
                    entry.Property(date).CurrentValue = DateTime.Now;

                if (entry.State == EntityState.Modified)
                    entry.Property(date).IsModified = false;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}