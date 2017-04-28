using DinePlan.DineConnect.Dto;

namespace DinePlan.DineConnect.Common.Dto
{
    public class FindUsersInput : PagedAndFilteredInputDto
    {
        public int? TenantId { get; set; }
    }
}