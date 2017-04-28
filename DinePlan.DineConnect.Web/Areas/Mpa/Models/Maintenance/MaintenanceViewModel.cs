using System.Collections.Generic;
using DinePlan.DineConnect.Caching.Dto;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Models.Maintenance
{
    public class MaintenanceViewModel
    {
        public IReadOnlyList<CacheDto> Caches { get; set; }
    }
}