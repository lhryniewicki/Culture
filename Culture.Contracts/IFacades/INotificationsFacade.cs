using Culture.Contracts.Dtos.NotificationWebJob;
using Culture.Contracts.ViewModels;
using System.Threading.Tasks;

namespace Culture.Contracts.Facades
{
    public interface INotificationsFacade
    {
        Task<int> GetNumberOfNotifications();
        Task<NotificationListViewModel> GetNotifications(int page);
        Task CreateNotificationsWebJob(NotificationWebJob notificationWebJob);
        Task MarkAsRead(int notificationId);
    }
}
