using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.Authorization.Permissions.Dto;

namespace DinePlan.DineConnect.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultOutput<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
