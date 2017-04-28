using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using DinePlan.DineConnect.Web.Controllers;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class WelcomeController : DineConnectControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}