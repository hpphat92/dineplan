using Abp.AutoMapper;
using DinePlan.DineConnect.Authorization.Roles.Dto;
using DinePlan.DineConnect.Web.Areas.Mpa.Models.Common;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode
        {
            get { return Role.Id.HasValue; }
        }

        public CreateOrEditRoleModalViewModel(GetRoleForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}