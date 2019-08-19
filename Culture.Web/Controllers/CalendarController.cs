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
    public class CalendarController : Controller
    {
        private readonly ICalendarService _calendarService;
        private readonly IUserService _userService;
        private readonly IEventService _eventService;
        public CalendarController(
            ICalendarService calendarService,
            IUserService userService,
            IEventService eventService)
        {
            _calendarService = calendarService;
            _userService = userService;
            _eventService = eventService;
        }
        [HttpPost("signuser")]
        public async Task<IActionResult> SignUserToEvent([FromBody]int eventId)
        {

            try
            {
                var user = await _userService.GetUserByName("lukasz");
                var _event = await _eventService.GetEventAsync(eventId);

                await _calendarService.SignUserToEvent(eventId,user);
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