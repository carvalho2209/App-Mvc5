using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AAC.AppMvc.ViewModels;
using AAC.Business.Core.Notifications;
using AAC.Business.Models.Products;
using AAC.Business.Models.Products.Services;
using AAC.Business.Models.Providers;
using AAC.AppMvc.Extensions;

namespace AAC.AppMvc.Controllers
{
    [Authorize]
    public class ProductsController : BaseController
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(
            IProductRepository productRepository,
            IProductService productService,
            IProviderRepository providerRepository,
            IMapper mapper, INotificator notificator)
            : base(notificator)
        {
            _providerRepository = providerRepository;
            _productRepository = productRepository;
            _productService = productService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("product-list")]
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var products = _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetProductsProviders());

            return View(products);
        }

        [AllowAnonymous]
        [Route("product-data/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Details(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
                return HttpNotFound();

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product", "Add")] 
        [Route("product-new")]
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var product = await PopulateProviders(new ProductViewModel());

            return View(product);
        }

        [ClaimsAuthorize("Product", "Add")]
        [Route("product-new")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel productViewModel)
        {
            productViewModel = await PopulateProviders(productViewModel);
            if (!ModelState.IsValid) return View(productViewModel);

            var imgPrefx = Guid.NewGuid() + "_";
            if (!UploadImage(productViewModel.ImageUpload, imgPrefx))
                return View(productViewModel);

            productViewModel.Image = imgPrefx + productViewModel.ImageUpload.FileName;
            
            await _productService.Add(_mapper.Map<Product>(productViewModel));

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Product", "Edit")]
        [Route("product-edit/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Edit(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
                return HttpNotFound();

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product", "Edit")]
        [Route("product-edit/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductViewModel productViewModel)
        {
            if (!ModelState.IsValid)
                return View(productViewModel);

            var productUpdated = await GetProduct(productViewModel.Id);
            productViewModel.Image = productUpdated.Image;

            if (productViewModel.ImageUpload != null)
            {
                var impPrefix = Guid.NewGuid() + "_";
                if (!UploadImage(productViewModel.ImageUpload, impPrefix))
                    return View(productViewModel);

                productUpdated.Image = impPrefix + productViewModel.ImageUpload.FileName;
            }

            productUpdated.Name = productViewModel.Name;
            productUpdated.Description = productViewModel.Description;
            productUpdated.Amount = productViewModel.Amount;
            productUpdated.Active = productViewModel.Active;
            productUpdated.ProviderId = productViewModel.ProviderId;
            productUpdated.Provider = productViewModel.Provider;

            await _productService.Update(_mapper.Map<Product>(productViewModel));

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Product", "Delete")]
        [Route("product-delete/{id:guid}")]
        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
                return HttpNotFound();

            return View(productViewModel);
        }

        [ClaimsAuthorize("Product", "Delete")]
        [Route("product-delete/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            var productViewModel = await GetProduct(id);

            if (productViewModel == null)
                return HttpNotFound();

            await _productService.Remove(id);

            return RedirectToAction("Index");
        }

        private async Task<ProductViewModel> GetProduct(Guid id)
        {
            var product = _mapper.Map<ProductViewModel>(await _productRepository.GetProductProvider(id));
            product.Providers = _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());

            return product;
        }

        private async Task<ProductViewModel> PopulateProviders(ProductViewModel productViewModel)
        {
            productViewModel.Providers =
                _mapper.Map<IEnumerable<ProviderViewModel>>(await _providerRepository.GetAll());

            return productViewModel;
        }

        private bool UploadImage(HttpPostedFileBase img, string imgPrefx)
        {
            if (img == null || img.ContentLength <= 0)
            {
                ModelState.AddModelError(string.Empty, "invalid format image");
                return false;
            }

            var path = Path.Combine(HttpContext.Server.MapPath("~/Images"), imgPrefx + img.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "there is already a file with this name");
                return false;
            }

            img.SaveAs(path);
            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _productRepository.Dispose();
                _productService.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
