﻿using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.Organizations.Dto;

namespace DinePlan.DineConnect.Organizations
{
    public interface IOrganizationUnitAppService : IApplicationService
    {
        Task<ListResultOutput<OrganizationUnitDto>> GetOrganizationUnits();

        Task<PagedResultOutput<OrganizationUnitUserListDto>> GetOrganizationUnitUsers(GetOrganizationUnitUsersInput input);

        Task<OrganizationUnitDto> CreateOrganizationUnit(CreateOrganizationUnitInput input);

        Task<OrganizationUnitDto> UpdateOrganizationUnit(UpdateOrganizationUnitInput input);

        Task<OrganizationUnitDto> MoveOrganizationUnit(MoveOrganizationUnitInput input);

        Task DeleteOrganizationUnit(IdInput<long> input);

        Task AddUserToOrganizationUnit(UserToOrganizationUnitInput input);
        
        Task RemoveUserFromOrganizationUnit(UserToOrganizationUnitInput input);

        Task<bool> IsInOrganizationUnit(UserToOrganizationUnitInput input);
    }
}
