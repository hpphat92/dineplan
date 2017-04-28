using Abp.Application.Services;
using DinePlan.DineConnect.Tenants.Dashboard.Dto;

namespace DinePlan.DineConnect.Tenants.Dashboard
{
    public interface ITenantDashboardAppService : IApplicationService
    {
        GetMemberActivityOutput GetMemberActivity();
    }
}
