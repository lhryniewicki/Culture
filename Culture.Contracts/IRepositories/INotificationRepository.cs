using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
    public interface INotificationRepository
    {
        Task<int> GetNumberOfUnreadNotifications(Guid userId);
        Task CreateNotification(Notification notification);
        Task<IEnumerable<Notification>> GetNotificationsForUserAsync(Guid userId, int skip = 0, int take = 5);
        Task<Notification> GetNotificationAsync(int id);

    }
}
