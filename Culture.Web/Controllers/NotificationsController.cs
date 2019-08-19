using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Culture.Contracts.DTOs;
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

        public NotificationsController(
            IUserService userService,
            INotificationService notificationService)
        {
            _userService = userService;
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<JsonResult> GetNumberOfNotifications()
        {
            var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
            var numberOfNotifications = await _notificationService.GetNumberOfUnreadNotifications(user.Id);

            return Json(numberOfNotifications);
        }
        [HttpPost]
        public async Task<JsonResult> CreateNotificationsWebJob([FromBody]NotificationWebJob notificationWebJob)
        {
           var notification= await _notificationService.CreateNotificationsAsync(notificationWebJob.Content, notificationWebJob.TargetUsers, notificationWebJob.EventId);
           await _notificationService.Commit();

            return Json(notification);
        }
        [HttpPut]
        public async Task MarkAsRead([FromBody]int notificationId)
        {
            await _notificationService.MarkAsRead(notificationId);
            await _notificationService.Commit();
        }
    }
}