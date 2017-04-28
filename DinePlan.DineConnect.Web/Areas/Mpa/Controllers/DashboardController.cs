using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using DinePlan.DineConnect.Authorization;
using DinePlan.DineConnect.Web.Controllers;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize(AppPermissions.Pages_Tenant_Dashboard)]
    public class DashboardController : DineConnectControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}