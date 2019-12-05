using Culture.Contracts.DTOs;
using Culture.Contracts.Facades;
using Culture.Contracts.IServices;
using Culture.Utilities.Enums;
using Culture.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Implementation.Facades
{
    public class AttendanceFacade : IAttendanceFacade
    {
        private readonly ICalendarService _calendarService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AttendanceFacade(
            ICalendarService calendarService,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _calendarService = calendarService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task AddToEventCalendar(int eventId)
        {

            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var user = await _userService.GetUserById(userId);

            await _calendarService.AddToCalendar(eventId, user.CalendarId);

            await _calendarService.Commit();
        }

        public async Task<IEnumerable<DateTime>> GetUserCalendarDays(string category = null, string query = null)
        {

            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var userCalendarDays = await _userService.GetUserCalendarDays(Guid.Parse(userId), category, query);

            return userCalendarDays;
        }

        public async Task<IEnumerable<EventInCalendarDto>> GetUserCalendarDays(DateTime date)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var userCalendarEventsDay = await _userService.GetUserEventsInDay(Guid.Parse(userId), date);

            return userCalendarEventsDay;
        }

        public async Task RemoveEventFromCalendar(int eventId)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            await _calendarService.RemoveEventFromCalendar(eventId, Guid.Parse(userId));

            await _calendarService.Commit();

        }

        public async Task RemoveUserFromSigned(int eventId)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            await _calendarService.UnsignUserFromEvent(eventId, Guid.Parse(userId));
            var isEventInCalendar = await _calendarService.CheckIfExists(eventId, Guid.Parse(userId));

            if (isEventInCalendar)
                await _calendarService.RemoveEventFromCalendar(eventId, Guid.Parse(userId));

            await _calendarService.Commit();
        }

        public async Task SignUserToEvent(int eventId)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var user = await _userService.GetUserById(userId);

            await _calendarService.SignUserToEvent(eventId, user.Id);
            var isEventInCalendar = await _calendarService.CheckIfExists(eventId, user.Id);

            if (!isEventInCalendar)
                await _calendarService.AddToCalendar(eventId, user.CalendarId);

            await _calendarService.Commit();
        }
    }
}
