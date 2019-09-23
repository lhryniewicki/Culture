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
        Task Commit();

    }
}
