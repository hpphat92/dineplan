using Abp.Notifications;
using DinePlan.DineConnect.Dto;

namespace DinePlan.DineConnect.Notifications.Dto
{
    public class GetUserNotificationsInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}