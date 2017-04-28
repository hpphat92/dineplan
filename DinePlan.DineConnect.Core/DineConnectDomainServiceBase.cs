using Abp.Domain.Services;

namespace DinePlan.DineConnect
{
    public abstract class DineConnectDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected DineConnectDomainServiceBase()
        {
            LocalizationSourceName = DineConnectConsts.LocalizationSourceName;
        }
    }
}
