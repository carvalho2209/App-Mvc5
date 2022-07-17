using System;
using System.Threading.Tasks;

namespace AAC.Business.Models.Products.Services
{
    public interface IProductService : IDisposable
    {
        Task Add(Product product);
        Task Update(Product product);
        Task Remove(Guid id); 
    }
}