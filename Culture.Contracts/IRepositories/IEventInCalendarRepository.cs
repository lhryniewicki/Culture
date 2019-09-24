using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
    public interface IEventInCalendarRepository
    {
        Task SignToCalendar(EventInCalendar eventInCalendar);
        Task RemoveFromCalendar(int eventId, Guid userId);
    }
}
