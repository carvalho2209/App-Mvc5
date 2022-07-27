using System;
using System.Linq;
using System.Threading.Tasks;
using AAC.Business.Core.Notifications;
using AAC.Business.Core.Services;
using AAC.Business.Models.Providers.Validations;

namespace AAC.Business.Models.Providers.Services
{
    public class ProviderService : BaseService, IProviderService
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IAddressRepository _addressRepository;

        public ProviderService(
            IProviderRepository providerRepository,
            IAddressRepository addressRepository,
            INotificator notificator) 
            : base(notificator)
        {
            _providerRepository = providerRepository;
            _addressRepository = addressRepository;
        }
        
        public async Task Add(Provider provider)
        {
            provider.Address.Id = provider.Id;
            provider.Address.Provider = provider;

            if (!ExecuteValidation(new ProviderValidation(), provider)
                || !ExecuteValidation(new AddressValidation(), provider.Address))
                return;

            if (await ProviderExists(provider))
                return;

            await _providerRepository.Add(provider);
        }

        public async Task Update(Provider provider)
        {
            if (!ExecuteValidation(new ProviderValidation(), provider))
                return;

            if (await ProviderExists(provider))
                return;

            await _providerRepository.Update(provider);
        }

        public async Task Remove(Guid id)
        {
            var provider = await _providerRepository.GetProviderProductAddress(id);

            if (provider.Products.Any())
            {
                Notificate("the supplier has registered products!");
                return;
            }

            if (provider.Address != null)
                await _addressRepository.Remove(provider.Address.Id);

            await _providerRepository.Remove(id);
        }

        private async Task<bool> ProviderExists(Provider provider)
        {
            var actualProvider =
                await _providerRepository.Search(p =>
                    p.Document == provider.Document && p.Id != provider.Id);

            if (!actualProvider.Any())
                return false;

            Notificate("there is already a supplier with this document!");
            return true;
        }

        public async Task UpdateAddress(Address address)
        {
            if (!ExecuteValidation(new AddressValidation(), address))
                return;

            await _addressRepository.Update(address);
        }

        public void Dispose()
        {
            _providerRepository?.Dispose();
            _addressRepository?.Dispose();
        }
    }
}