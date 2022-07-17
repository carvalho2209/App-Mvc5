using System;
using System.Threading.Tasks;
using AAC.Business.Core.Data;

namespace AAC.Business.Models.Providers
{
    public interface IProviderRepository : IRepository<Provider>
    {
        Task<Provider> GetProviderAddress(Guid id);
        Task<Provider> GetProviderProductAddress(Guid id);
    }
}
 