using System.Web.Mvc;

namespace DinePlan.DineConnect.Web.Controllers
{
    public class HomeController : DineConnectControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}