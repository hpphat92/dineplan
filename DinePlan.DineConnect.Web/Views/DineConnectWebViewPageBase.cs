using Abp.Dependency;
using Abp.Runtime.Session;
using Abp.Web.Mvc.Views;

namespace DinePlan.DineConnect.Web.Views
{
    public abstract class DineConnectWebViewPageBase : DineConnectWebViewPageBase<dynamic>
    {
       
    }

    public abstract class DineConnectWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        public IAbpSession AbpSession { get; private set; }
        
        protected DineConnectWebViewPageBase()
        {
            AbpSession = IocManager.Instance.Resolve<IAbpSession>();
            LocalizationSourceName = DineConnectConsts.LocalizationSourceName;
        }
    }
}