using Culture.Contracts.DTOs;
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
        Task<Notification> CreateNotificationsAsync(string content, IEnumerable<Guid> targetUsers, int eventId, string eventSlug);
        Task<Notification> MarkAsRead(int id);
        Task<IEnumerable<NotificationDto>> GetNotifications(Guid userId, int page = 0);
        Task Commit();

    }
}
