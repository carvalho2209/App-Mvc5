using AAC.AppMvc.ViewModels;
using System.Web.Mvc;

namespace AAC.AppMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public ActionResult Errors(int id)
        {
            var modelErro = new ErrorViewModel();

            switch (id)
            {
                case 500:
                    modelErro.Message = "An error has occurred! Please try again later or contact our support.";
                    modelErro.Title = "An error has occurred!";
                    modelErro.ErrorCode = id;
                    break;
                case 404:
                    modelErro.Message = "The page you are looking for does not exist! < br />If you have any questions, please contact our support.";
                    modelErro.Title = "Oops! Page not found.";
                    modelErro.ErrorCode = id;
                    break;
                case 403:
                    modelErro.Message = "You are not allowed to do this.";
                    modelErro.Title = "Access denied.";
                    modelErro.ErrorCode = id;
                    break;
                default:
                    return new HttpStatusCodeResult(500);
            }

            return View("Error", modelErro);
        }
    }
}