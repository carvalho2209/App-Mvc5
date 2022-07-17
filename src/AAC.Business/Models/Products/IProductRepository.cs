using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AAC.Business.Core.Data;

namespace AAC.Business.Models.Products
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductByProvider(Guid providerId);
        Task<IEnumerable<Product>> GetProductsProviders(); 
        Task<Product> GetProductProvider(Guid productId); 
    }
}