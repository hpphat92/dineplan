using System.Web.Mvc;
using Abp.Auditing;

namespace DinePlan.DineConnect.Web.Controllers
{
    public class ErrorController : DineConnectControllerBase
    {
        [DisableAuditing]
        public ActionResult E404()
        {
            return View();
        }
    }
}