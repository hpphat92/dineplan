using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.Caching.Dto;

namespace DinePlan.DineConnect.Caching
{
    public interface ICachingAppService : IApplicationService
    {
        ListResultOutput<CacheDto> GetAllCaches();

        Task ClearCache(IdInput<string> input);

        Task ClearAllCaches();
    }
}
