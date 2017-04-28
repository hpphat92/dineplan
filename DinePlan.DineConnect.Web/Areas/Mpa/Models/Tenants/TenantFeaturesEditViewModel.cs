using Abp.AutoMapper;
using DinePlan.DineConnect.MultiTenancy;
using DinePlan.DineConnect.MultiTenancy.Dto;
using DinePlan.DineConnect.Web.Areas.Mpa.Models.Common;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesForEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesForEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }

        public TenantFeaturesEditViewModel(Tenant tenant, GetTenantFeaturesForEditOutput output)
        {
            Tenant = tenant;
            output.MapTo(this);
        }
    }
}