using System.Collections.Generic;
using DinePlan.DineConnect.Authorization.Users.Dto;

namespace DinePlan.DineConnect.Web.Areas.Mpa.Models.Users
{
    public class UserLoginAttemptModalViewModel
    {
        public List<UserLoginAttemptDto> LoginAttempts { get; set; }
    }
}