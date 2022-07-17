using System;
using System.Data.Entity;
using System.Threading.Tasks;
using AAC.Business.Models.Providers;
using AAC.Infra.Data.Context;

namespace AAC.Infra.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(MyDbContext db, DbSet<Address> dbSet) 
            : base(db, dbSet)
        {
        }

        public async Task<Address> GetAddressByProvider(Guid providerId)
        {
            //return await GetById(providerId);

            return await Db.Address.AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == providerId);
        }
    }
}