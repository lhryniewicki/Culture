using Culture.Contracts.IRepositories;
using Culture.DataAccess.Context;
using Culture.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.DataAccess.Repositories
{
    public class EventInCalendarRepository : IEventInCalendarRepository
    {
        private readonly CultureDbContext _cultureDbContext;

        public EventInCalendarRepository( CultureDbContext cultureDbContext)
        {
            _cultureDbContext = cultureDbContext;
        }

        public async Task RemoveFromCalendar(int eventId, Guid userId)
        {
            var toDelete = await _cultureDbContext.EventsInCalendar
                .FirstOrDefaultAsync(x => x.EventId == eventId && x.Calendar.BelongsToId == userId);
            _cultureDbContext.EventsInCalendar.Remove(toDelete);
        }
        public Task SignToCalendar(EventInCalendar eventInCalendar)
        {
            return _cultureDbContext.EventsInCalendar.AddAsync(eventInCalendar);
        }
    }
}
