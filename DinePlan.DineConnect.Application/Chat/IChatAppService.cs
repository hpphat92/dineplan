using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using DinePlan.DineConnect.Chat.Dto;

namespace DinePlan.DineConnect.Chat
{
    public interface IChatAppService : IApplicationService
    {
        GetUserChatFriendsWithSettingsOutput GetUserChatFriendsWithSettings();

        Task<ListResultOutput<ChatMessageDto>> GetUserChatMessages(GetUserChatMessagesInput input);

        Task MarkAllUnreadMessagesOfUserAsRead(MarkAllUnreadMessagesOfUserAsReadInput input);
    }
}
