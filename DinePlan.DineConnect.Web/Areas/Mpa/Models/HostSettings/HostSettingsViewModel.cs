using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.Configuration.Host.Dto;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Models.HostSettings
{
    public class HostSettingsViewModel
    {
        public HostSettingsEditDto Settings { get; set; }

        public List<ComboboxItemDto> EditionItems { get; set; }

        public List<ComboboxItemDto> TimezoneItems { get; set; }
    }
}