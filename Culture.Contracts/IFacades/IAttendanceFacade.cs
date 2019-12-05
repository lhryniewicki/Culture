using Culture.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Culture.Contracts.Facades
{
    public interface IAttendanceFacade
    {
        Task SignUserToEvent(int eventId);
        Task RemoveUserFromSigned(int eventId);
        Task AddToEventCalendar(int eventId);
        Task RemoveEventFromCalendar(int eventId);
        Task<IEnumerable<DateTime>> GetUserCalendarDays(string category = null, string query = null);
        Task<IEnumerable<EventInCalendarDto>> GetUserCalendarDays(DateTime date);
    }
}
