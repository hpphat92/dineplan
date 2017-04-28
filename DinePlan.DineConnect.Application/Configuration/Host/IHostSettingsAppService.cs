using System.Threading.Tasks;
using Abp.Application.Services;
using DinePlan.DineConnect.Configuration.Host.Dto;

namespace DinePlan.DineConnect.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
