using Abp.Authorization;
using DinePlan.DineConnect.Authorization.Roles;
using DinePlan.DineConnect.Authorization.Users;
using DinePlan.DineConnect.MultiTenancy;

namespace DinePlan.DineConnect.Authorization
{
    /// <summary>
    /// Implements <see cref="PermissionChecker"/>.
    /// </summary>
    public class PermissionChecker : PermissionChecker<Tenant, Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
