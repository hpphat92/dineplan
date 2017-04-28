using Abp.AutoMapper;
using DinePlan.DineConnect.Authorization.Users;
using DinePlan.DineConnect.Authorization.Users.Dto;
using DinePlan.DineConnect.Web.Areas.Mpa.Models.Common;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; private set; }

        public UserPermissionsEditViewModel(GetUserPermissionsForEditOutput output, User user)
        {
            User = user;
            output.MapTo(this);
        }
    }
}