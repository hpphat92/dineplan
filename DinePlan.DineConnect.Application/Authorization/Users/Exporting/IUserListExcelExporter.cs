using System.Collections.Generic;
using DinePlan.DineConnect.Authorization.Users.Dto;
using DinePlan.DineConnect.Dto;

namespace DinePlan.DineConnect.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}