using System;
using System.Threading.Tasks;
using AAC.Business.Models.Providers;

namespace AAC.Business.Models.Services
{
    public interface IProviderService : IDisposable
    {
        Task Add(Provider provider);
        Task Update(Provider provider);
        Task Remove(Guid id);
        Task UpdateAddress(Address address);
    }
}