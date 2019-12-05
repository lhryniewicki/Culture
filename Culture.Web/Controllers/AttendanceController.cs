using System;
using System.Threading.Tasks;
using Culture.Contracts.Facades;
using Culture.Contracts.IServices;
using Culture.Utilities.Enums;
using Culture.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Culture.Web.Controllers
{
    [Authorize(Roles = "User,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : Controller
    {
        private readonly IAttendanceFacade _attendanceFacade;

        public AttendanceController(
            IAttendanceFacade attendanceFacade)
        {
            _attendanceFacade = attendanceFacade;
        }

        [HttpPost("user/sign")]
        public async Task<IActionResult> SignUserToEvent([FromBody]int eventId)
        {
            try
            {
                await _attendanceFacade.SignUserToEvent(eventId);

                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [HttpDelete("user/sign")]
        public async Task<IActionResult> RemoveUserFromSigned([FromBody]int eventId)
        {
            try
            {
                await _attendanceFacade.RemoveUserFromSigned(eventId);

                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [HttpPost("calendar")]
        public async Task<IActionResult> AddToEventCalendar([FromBody]int eventId)
        {
            try
            {
                await _attendanceFacade.AddToEventCalendar(eventId);

                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [HttpDelete("calendar")]
        public async Task<IActionResult> RemoveEventFromCalendar([FromBody]int eventId)
        {
            try
            {
                await _attendanceFacade.RemoveEventFromCalendar(eventId);

                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [HttpGet("get")]
        public async Task<JsonResult> GetUserCalendarDays(string category=null, string query=null)
        {
            try
            {
                var userCalendarDays = await _attendanceFacade.GetUserCalendarDays(category,query);

                return Json(userCalendarDays);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }

        }

        [HttpGet("get/{date}")]
        public async Task<JsonResult> GetUserCalendarDays([FromRoute]DateTime date)
        {
            try
            {
                var userCalendarEventsDay = await _attendanceFacade.GetUserCalendarDays(date);

                return Json(userCalendarEventsDay);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }

        }

    }
}