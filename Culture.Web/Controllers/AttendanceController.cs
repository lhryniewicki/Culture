using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Culture.Contracts.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Culture.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : Controller
    {
        private readonly ICalendarService _calendarService;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        public AttendanceController(
            ICalendarService calendarService,
            IUserService userService,
            IEventService eventService)
        {
            _calendarService = calendarService;
            _userService = userService;
            _eventService = eventService;
        }
        [HttpPost("user/sign")]
        public async Task<IActionResult> SignUserToEvent([FromBody]int eventId)
        {
            try
            {
                var user = await _userService.GetUserByName("maciek");

                await _calendarService.SignUserToEvent(eventId, user);
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

                await _calendarService.AddToCalendar(eventId, user);
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
        public async Task<JsonResult> GetUserCalendar()
        {
            try
            {
                var user = await _userService.GetUserByNameWithCalendar(/*HttpContext.User.Identity.Name*/"lukasz");
 
                return Json(user);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }

        }

    }
}