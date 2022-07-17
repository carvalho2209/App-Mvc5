using System.Threading.Tasks;
using System.Web.Mvc;
using AAC.Business.Models.Providers;
using AAC.Business.Models.Services;

namespace AAC.AppMvc.Controllers
{
    public class ProviderController : Controller
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        // GET: Provider
        public async Task<ActionResult> Index()
        {
            var provider = new Provider()
            {
                Name = "",
                Document = "123456",
                Address = new Address(),
                TypeProvider = TypeProvider.LegalPerson,
                Active = true
            };

            await _providerService.Add(provider);

            return View();
        }
    }
}