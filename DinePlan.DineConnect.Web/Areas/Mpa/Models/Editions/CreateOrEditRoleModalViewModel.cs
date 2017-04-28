using Abp.AutoMapper;
using DinePlan.DineConnect.Editions.Dto;
using DinePlan.DineConnect.Web.Areas.Mpa.Models.Common;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Models.Editions
{
    [AutoMapFrom(typeof(GetEditionForEditOutput))]
    public class CreateOrEditEditionModalViewModel : GetEditionForEditOutput, IFeatureEditViewModel
    {
        public bool IsEditMode
        {
            get { return Edition.Id.HasValue; }
        }

        public CreateOrEditEditionModalViewModel(GetEditionForEditOutput output)
        {
            output.MapTo(this);
        }
    }
}