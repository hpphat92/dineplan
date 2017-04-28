using System.Collections.Generic;
using DinePlan.DineConnect.Authorization.Permissions.Dto;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}