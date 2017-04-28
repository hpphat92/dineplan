using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.Configuration.Tenants.Dto;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Models.Settings
{
    public class SettingsViewModel
    {
        public TenantSettingsEditDto Settings { get; set; }
        
        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}