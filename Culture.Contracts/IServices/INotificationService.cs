using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
    public interface INotificationService
    {
        Task<int> GetNumberOfUnreadNotifications(Guid userId);
        Task<Notification> CreateNotificationsAsync(string content, IEnumerable<Guid> targetUsers, int eventId);
        Task<Notification> MarkAsRead(int id);
        Task Commit();

    }
}
