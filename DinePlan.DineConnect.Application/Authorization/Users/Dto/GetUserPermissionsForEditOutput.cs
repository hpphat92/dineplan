using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.Authorization.Permissions.Dto;

namespace DinePlan.DineConnect.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}