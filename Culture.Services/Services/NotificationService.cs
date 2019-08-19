using Culture.Contracts;
using Culture.Contracts.IServices;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<int> GetNumberOfUnreadNotifications(Guid userId)
        {
            return _unitOfWork.NotificationRepository.GetNumberOfUnreadNotifications(userId);
        }
        public async Task<Notification> CreateNotificationsAsync(string content,IEnumerable<Guid> targetUsers, int eventId)
        {
            var notification = new Notification()
            {
                IsRead = false,
                Content = content,
                EventId = eventId,
                SentData = DateTime.Now
            };
            foreach (var userId in targetUsers)
            {
                notification.UserId = userId;
                await _unitOfWork.NotificationRepository.CreateNotification(notification);
            }
            return notification;

        }
        public async Task<Notification> MarkAsRead(int id)
        {
            var notification = await _unitOfWork.NotificationRepository.GetNotificationAsync(id);

            notification.IsRead = true;


            return null;
        }
        public Task Commit()
        {
            return _unitOfWork.Commit();
        }
    }
}
