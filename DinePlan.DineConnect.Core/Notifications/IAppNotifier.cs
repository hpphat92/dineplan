using System.Threading.Tasks;
using Abp;
using Abp.Notifications;
using DinePlan.DineConnect.Authorization.Users;
using DinePlan.DineConnect.MultiTenancy;

namespace DinePlan.DineConnect.Notifications
{
    public interface IAppNotifier
    {
        Task WelcomeToTheApplicationAsync(User user);

        Task NewUserRegisteredAsync(User user);

        Task NewTenantRegisteredAsync(Tenant tenant);

        Task SendMessageAsync(UserIdentifier user, string message, NotificationSeverity severity = NotificationSeverity.Info);
    }
}
