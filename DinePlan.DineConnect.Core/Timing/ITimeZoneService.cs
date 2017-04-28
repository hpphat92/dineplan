using System.Threading.Tasks;
using Abp.Configuration;

namespace DinePlan.DineConnect.Timing
{
    public interface ITimeZoneService
    {
        Task<string> GetDefaultTimezoneAsync(SettingScopes scope, int? tenantId);
    }
}
