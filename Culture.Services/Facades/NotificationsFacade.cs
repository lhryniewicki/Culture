using Culture.Contracts.Dtos.NotificationWebJob;
using Culture.Contracts.Facades;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Utilities.Enums;
using Culture.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Implementation.Facades
{
    public class NotificationsFacade : INotificationsFacade
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
        private readonly IEventService _eventService;
        private readonly IHttpContextAccessor _httpcontextaccessor;

        public NotificationsFacade(
            IUserService userService,
            INotificationService notificationService,
            IEventService eventService,
            IHttpContextAccessor httpcontextaccessor)
        {
            _userService = userService;
            _notificationService = notificationService;
            _eventService = eventService;
            _httpcontextaccessor = httpcontextaccessor;
        }
        public async Task CreateNotificationsWebJob(NotificationWebJob notificationWebJob)
        {
            foreach (var _event in notificationWebJob.Notifications)
            {
                var targetEvent = await _eventService.GetEventAsync(_event.EventId);

                var notification = await _notificationService.CreateNotificationsAsync($"Masz nadchodzace wydarzenie: {targetEvent.Name}!", _event.TargetUsers, _event.EventId, null);
            }
            await _notificationService.Commit();
        }

        public async Task<NotificationListViewModel> GetNotifications(int page)
        {
            var userId = _httpcontextaccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var notifications = await _notificationService.GetNotifications(Guid.Parse(userId), page);

            await _notificationService.Commit();

            return new NotificationListViewModel(notifications);
        }

        public async Task<int> GetNumberOfNotifications()
        {
            var userId = _httpcontextaccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            return await _notificationService.GetNumberOfUnreadNotifications(Guid.Parse(userId));
        }

        public async Task MarkAsRead(int notificationId)
        {
            await _notificationService.MarkAsRead(notificationId);

            await _notificationService.Commit();
        }
    }
}
