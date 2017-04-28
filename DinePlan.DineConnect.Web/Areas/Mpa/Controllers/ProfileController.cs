using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Configuration;
using Abp.Web.Mvc.Authorization;
using DinePlan.DineConnect.Authorization.Users.Profile;
using DinePlan.DineConnect.Timing;
using DinePlan.DineConnect.Timing.Dto;
using DinePlan.DineConnect.Web.Areas.Mpa.Models.Profile;
using DinePlan.DineConnect.Web.Controllers;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : DineConnectControllerBase
    {
        private readonly IProfileAppService _profileAppService;
        private readonly ITimingAppService _timingAppService;


        public ProfileController(
            IProfileAppService profileAppService, 
            ITimingAppService timingAppService)
        {
            _profileAppService = profileAppService;
            _timingAppService = timingAppService;
        }

        public async Task<PartialViewResult> MySettingsModal()
        {
            var output = await _profileAppService.GetCurrentUserProfileForEdit();
            var timezoneItems = await _timingAppService.GetTimezoneComboboxItems(new GetTimezoneComboboxItemsInput
            {
                DefaultTimezoneScope = SettingScopes.User,
                SelectedTimezoneId = output.Timezone
            });

            var viewModel = new MySettingsViewModel(output)
            {
                TimezoneItems = timezoneItems
            };

            return PartialView("_MySettingsModal", viewModel);
        }

        public PartialViewResult ChangePictureModal()
        {
            return PartialView("_ChangePictureModal");
        }

        public PartialViewResult ChangePasswordModal()
        {
            return PartialView("_ChangePasswordModal");
        }

        public PartialViewResult LinkedAccountsModal()
        {
            return PartialView("_LinkedAccountsModal");
        }

        public PartialViewResult LinkAccountModal()
        {
            return PartialView("_LinkAccountModal");
        }
    }
}