using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using AAC.AppMvc.Extensions;
using AAC.AppMvc.ViewModels;
using AAC.Business.Core.Notifications;
using AAC.Business.Models.Providers;
using AAC.Business.Models.Providers.Services;
using AutoMapper;

namespace AAC.AppMvc.Controllers
{
    [Authorize]
    public class ProvidersController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProviderService _providerService;
        private readonly IMapper _mapper;

        public ProvidersController( IProviderRepository providerRepository,
                                    IProviderService providerService,
                                    IMapper mapper,
                                    INotificator notificator) : base(notificator)
        {
            _providerRepository = providerRepository;
            _providerService = providerService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("provider-list")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll()));
        }

        [AllowAnonymous]
        [Route("provider-details/{id:guid}")]
        public async Task<ActionResult> Details(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null)
                return HttpNotFound();

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider","Add")]
        [Route("provider-new")]
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [ClaimsAuthorize("Provider", "Add")]
        [Route("provider-new")]
        [HttpPost]
        public async Task<ActionResult> Create(ProviderViewModel providerViewModel)
        {
            if (!ModelState.IsValid)
                return View(providerViewModel);

            var provider =  _mapper.Map<Provider>(providerViewModel);
            await _providerService.Add(provider);

            if (!ValidOperation())
                return View(providerViewModel);

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Provider", "Edit")]
        [Route("provider-edit/{id:guid}")]
        public async Task<ActionResult> Edit(Guid id)
        {
            var providerViewModel = await GetProviderProductAddress(id);

            if (providerViewModel == null)
                return HttpNotFound();

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider", "Edit")]
        [Route("provider-edit/{id:guid}")]
        [HttpPost]
        public async Task<ActionResult> Edit(Guid id, ProviderViewModel providerViewModel)
        {
            if (id != providerViewModel.Id)
                return HttpNotFound();

            if (!ModelState.IsValid)
                return View(providerViewModel);

            var provider = _mapper.Map<Provider>(providerViewModel);
            await _providerService.Update(provider);

            if (!ValidOperation())
                return View(await GetProviderProductAddress(id));

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Provider", "Delete")]
        [Route("provider-delete/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null)
                return HttpNotFound();

            return View(providerViewModel);
        }

        [ClaimsAuthorize("Provider", "Delete")]
        [Route("provider-delete/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(Guid id) 
        {
            var providerViewModel = await GetProviderAddress(id);

            if (providerViewModel == null)
                return HttpNotFound();

            await _providerService.Remove(id);

            if (!ValidOperation()) 
                return View(providerViewModel);

            return RedirectToAction("Index");
        }

        [Route("provider-get-address/{id:guid}")]
        public async Task<ActionResult> GetAddress(Guid id)
        {
            var provider = await GetProviderAddress(id);

            if (provider == null)
            {
                return HttpNotFound();
            }

            return PartialView("_AddressDetails", provider);
        }
        
        [Route("provider-update-address/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> UpdateAddress(Guid id) 
        {
            var provider = await GetProviderAddress(id);

            if (provider == null)
            {
                return HttpNotFound();
            }

            return PartialView("_AddressUpdate", new ProviderViewModel() { Address = provider.Address });
        }

        [Route("provider-update-address/{id:guid}")]
        [HttpPost]
        public async Task<ActionResult> UpdateAddress(ProviderViewModel providerViewModel)
        {
            ModelState.Remove("Name");
            ModelState.Remove("Document"); 

            if (!ModelState.IsValid) return PartialView("_AddressUpdate", providerViewModel);

            await _providerService.UpdateAddress(_mapper.Map<Address>(providerViewModel.Address));
            
            var url = Url.Action("GetAddress", "Providers", new { id = providerViewModel.Address.ProviderId });
            return Json(new { success = true, url });
        }

        private async Task<ProviderViewModel> GetProviderAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderAddress(id));
        }

        private async Task<ProviderViewModel> GetProviderProductAddress(Guid id)
        {
            return _mapper.Map<ProviderViewModel>(await _providerRepository.GetProviderProductAddress(id));
        }
    }
}