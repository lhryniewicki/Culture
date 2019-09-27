using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Culture.Contracts.Dtos.NotificationWebJob;
using Culture.Contracts.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Culture.Web.Controllers
{
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

        [HttpGet]
        public async Task<JsonResult> GetNumberOfNotifications()
        {
            var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
            var numberOfNotifications = await _notificationService.GetNumberOfUnreadNotifications(user.Id);

            return Json(numberOfNotifications);
        }
        [HttpPost]
        public async Task CreateNotificationsWebJob([FromBody]NotificationWebJob notificationWebJob)
        {
			foreach(var _event in notificationWebJob.Notifications)
			{
				var targetEvent = await _eventService.GetEventAsync(_event.EventId);
				var notification = await _notificationService.CreateNotificationsAsync($"Masz nadchodzace wydarzenie: {targetEvent.Name}!", _event.TargetUsers, _event.EventId);
			}
			await _notificationService.Commit();
        }
        [HttpPut]
        public async Task MarkAsRead([FromBody]int notificationId)
        {
            await _notificationService.MarkAsRead(notificationId);
            await _notificationService.Commit();
        }
    }
}