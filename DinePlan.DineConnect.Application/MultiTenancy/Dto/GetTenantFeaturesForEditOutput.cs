using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.Editions.Dto;

namespace DinePlan.DineConnect.MultiTenancy.Dto
{
    public class GetTenantFeaturesForEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}