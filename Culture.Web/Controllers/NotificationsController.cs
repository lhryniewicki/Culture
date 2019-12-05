using Culture.Contracts.Dtos.NotificationWebJob;
using Culture.Contracts.Facades;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Utilities.Enums;
using Culture.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Culture.Web.Controllers
{
    [Authorize(Roles ="User,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : Controller
    {

        private readonly INotificationsFacade _notificationsFacade;

        public NotificationsController(INotificationsFacade notificationsFacade)
        {
            _notificationsFacade = notificationsFacade;
        }

        [HttpGet("number")]
        public async Task<JsonResult> GetNumberOfNotifications()
        {
            try
            {
                var numberOfNotifications = await _notificationsFacade.GetNumberOfNotifications();

                return Json(numberOfNotifications);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
           
        }

        [HttpGet("get")]
        public async Task<JsonResult> GetNotifications(int page)
        {
            try
            {
                return Json(await _notificationsFacade.GetNotifications(page));
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [HttpPost("create")]
        public async Task CreateNotificationsWebJob([FromBody]NotificationWebJob notificationWebJob)
        {
            try
            {
                await _notificationsFacade.CreateNotificationsWebJob(notificationWebJob);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
            }
        }

        [HttpPut("read")]
        public async Task MarkAsRead([FromBody]int notificationId)
        {
            try
            {
                await _notificationsFacade.MarkAsRead(notificationId);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
            }
        }
    }
}