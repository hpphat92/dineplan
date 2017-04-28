using System.Threading.Tasks;
using Abp.Application.Services;
using DinePlan.DineConnect.Configuration.Tenants.Dto;

namespace DinePlan.DineConnect.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);
    }
}
