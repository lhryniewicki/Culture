using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Culture.Contracts.Dtos.NotificationWebJob;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Utilities.Enums;
using Culture.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Culture.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : Controller
    {
        private readonly IUserService _userService;
        private readonly INotificationService _notificationService;
		private readonly IEventService _eventService;

		public NotificationsController(
            IUserService userService,
            INotificationService notificationService,
			IEventService eventService)
        {
            _userService = userService;
            _notificationService = notificationService;
			_eventService = eventService;
		}

        [HttpGet("number")]
        public async Task<JsonResult> GetNumberOfNotifications()
        {
            var userId = User.GetClaim(JwtTypes.jti);

            var numberOfNotifications = await _notificationService.GetNumberOfUnreadNotifications(Guid.Parse(userId));

            return Json(numberOfNotifications);
        }

        [HttpGet("get")]
        public async Task<JsonResult> GetNotifications(int page)
        {
            var userId = User.GetClaim(JwtTypes.jti);

            var notifications = await _notificationService.GetNotifications(Guid.Parse(userId),page);

            await _notificationService.Commit();

            return Json(new NotificationListViewModel(notifications));
        }

        [HttpPost("create")]
        public async Task CreateNotificationsWebJob([FromBody]NotificationWebJob notificationWebJob)
        {
			foreach(var _event in notificationWebJob.Notifications)
			{
				var targetEvent = await _eventService.GetEventAsync(_event.EventId);

				var notification = await _notificationService.CreateNotificationsAsync($"Masz nadchodzace wydarzenie: {targetEvent.Name}!", _event.TargetUsers, _event.EventId,null);
			}
			await _notificationService.Commit();
        }

        [HttpPut("read")]
        public async Task MarkAsRead([FromBody]int notificationId)
        {
            await _notificationService.MarkAsRead(notificationId);

            await _notificationService.Commit();
        }
    }
}