using Culture.Contracts;
using Culture.Contracts.DTOs;
using Culture.Contracts.IServices;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IEnumerable<NotificationDto>> GetNotifications(Guid userId,int page = 0)
        {
            var notifications = await _unitOfWork.NotificationRepository.GetNotificationsForUserAsync(userId, page);

            foreach(var notification in notifications)
            {
                if (notification.IsRead == false) notification.IsRead = true;
            }
            return notifications.Select(x => new NotificationDto()
            {
                Content = x.Content,
                Date = x.SentData.ToString("dd-MM-yyyy HH:mm"),
                RedirectUrl = x.RedirectUrl
            });
        }
        public async Task<Notification> CreateNotificationsAsync(string content,IEnumerable<Guid> targetUsers, int eventId, string eventSlug)
        {

            var notification = new Notification()
            {
                IsRead = false,
                Content = content,
                EventId = eventId,
                SentData = DateTime.Now,
                RedirectUrl = eventSlug
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
