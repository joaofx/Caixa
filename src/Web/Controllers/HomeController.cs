namespace Web.Controllers
{
    using System.Web.Mvc;
    using Helpers;

    public class HomeController : Controller
    {
        [ComMovimento]
        public ActionResult Index()
        {
            return View();
        }
    }
}
