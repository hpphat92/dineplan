using System.Collections.Generic;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.Editions.Dto;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}