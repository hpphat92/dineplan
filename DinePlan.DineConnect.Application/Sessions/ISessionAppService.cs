using System.Threading.Tasks;
using Abp.Application.Services;
using DinePlan.DineConnect.Sessions.Dto;

namespace DinePlan.DineConnect.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
