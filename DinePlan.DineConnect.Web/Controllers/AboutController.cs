using System.Web.Mvc;

namespace DinePlan.DineConnect.Web.Controllers
{
    public class AboutController : DineConnectControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}