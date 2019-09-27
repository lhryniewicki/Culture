using System;
using System.Threading.Tasks;
using Culture.Contracts.IServices;
using Microsoft.AspNetCore.Mvc;

namespace Culture.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : Controller
    {
        private readonly ICalendarService _calendarService;
        private readonly IUserService _userService;

        public AttendanceController(
            ICalendarService calendarService,
            IUserService userService)
        {
            _calendarService = calendarService;
            _userService = userService;
        }

        [HttpPost("user/sign")]
        public async Task<IActionResult> SignUserToEvent([FromBody]int eventId)
        {
            try
            {
                var user = await _userService.GetUserByName("maciek");

                await _calendarService.SignUserToEvent(eventId, user.Id);
                var isEventInCalendar = _calendarService.CheckIfExists(eventId, user.Id);

                await _calendarService.AddToCalendar(eventId, user.CalendarId);

                await _calendarService.Commit();

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
                var user = await _userService.GetUserByName("maciek");

                await _calendarService.UnsignUserFromEvent(eventId, user.Id);
                await _calendarService.RemoveEventFromCalendar(eventId, user.Id);

                await _calendarService.Commit();

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
                var user = await _userService.GetUserByName("maciek");

                await _calendarService.AddToCalendar(eventId, user.CalendarId);
                await _calendarService.Commit();

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
                var user = await _userService.GetUserByName("maciek");

                await _calendarService.RemoveEventFromCalendar(eventId,user.Id);

                await _calendarService.Commit();

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
                var user = await _userService.GetUserByName("maciek");

                var userCalendarDays = await _userService.GetUserCalendarDays(user.Id,category,query);

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
                var user = await _userService.GetUserByName("maciek");

                var userCalendarEventsDay = await _userService.GetUserEventsInDay(user.Id,date);

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