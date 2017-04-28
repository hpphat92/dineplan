using Abp.WebApi.Controllers;

namespace DinePlan.DineConnect.WebApi
{
    public abstract class DineConnectApiControllerBase : AbpApiController
    {
        protected DineConnectApiControllerBase()
        {
            LocalizationSourceName = DineConnectConsts.LocalizationSourceName;
        }
    }
}