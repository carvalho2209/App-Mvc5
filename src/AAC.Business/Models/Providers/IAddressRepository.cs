using System;
using System.Threading.Tasks;
using AAC.Business.Core.Data;

namespace AAC.Business.Models.Providers
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> GetAddressByProvider(Guid providerId);
    }
}
