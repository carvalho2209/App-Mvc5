using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AAC.Business.Models.Products;
using AAC.Infra.Data.Context;

namespace AAC.Infra.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(MyDbContext db) : base(db) { }
        
        public async Task<Product> GetProductProvider(Guid productId)
        {
            return await Db.Products.AsNoTracking().Include(p => p.Provider)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }
        public async Task<IEnumerable<Product>> GetProductsProviders()
        {
            return await Db.Products.AsNoTracking()
                .Include(p => p.Provider)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByProvider(Guid providerId)
        {
            return await Search(p => p.ProviderId == providerId);
        }
    }
}