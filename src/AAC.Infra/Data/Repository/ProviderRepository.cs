using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AAC.Business.Models.Providers;
using AAC.Infra.Data.Context;

namespace AAC.Infra.Data.Repository
{
    public class ProviderRepository : Repository<Provider>, IProviderRepository
    {
        public ProviderRepository(MyDbContext db, DbSet<Provider> dbSet) 
            : base(db, dbSet)
        {
        }

        public async Task<Provider> GetProviderAddress(Guid id)
        {
            return await Db.Providers.AsNoTracking()
                .Include(p => p.Address)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Provider> GetProviderProductAddress(Guid id)
        {
            return await Db.Providers.AsNoTracking()
                .Include(p => p.Address)
                .Include(P => P.Products)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task Remove(Guid id)
        {
            var provider = await GetById(id);
            provider.Active = false;

            await Update(provider);
        }
    }
}