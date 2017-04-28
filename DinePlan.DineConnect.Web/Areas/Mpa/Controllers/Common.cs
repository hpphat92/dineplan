using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;
using DinePlan.DineConnect.Web.Areas.Mpa.Models.Common.Modals;
using DinePlan.DineConnect.Web.Controllers;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class CommonController : DineConnectControllerBase
    {
        public PartialViewResult LookupModal(LookupModalViewModel model)
        {
            return PartialView("Modals/_LookupModal", model);
        }
    }
}