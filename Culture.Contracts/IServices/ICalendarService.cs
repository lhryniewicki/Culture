using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
    public interface ICalendarService
    {
        Task SignUserToEvent(int eventId, Guid userId);
        Task UnsignUserFromEvent(int eventId, Guid userId);
        Task AddToCalendar(int eventId, int calendarId);
        Task RemoveEventFromCalendar(int eventId, Guid userId);
        Task<IEnumerable<DateTime>> GetUserCalendarDays(Guid userId, string category, string query);
        Task<bool> CheckIfExists(int eventId, Guid userId);
        Task Commit();
    }
}
