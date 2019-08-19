using Culture.Contracts;
using Culture.Contracts.IServices;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalendarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task SignUserToEvent(int eventId,AppUser user)
        {
            var userEvent = new UserInEvent()
            {
                EventId = eventId,
                UserId = user.Id
            };
            user.ParticipatedEvents.Add(userEvent);
        }
        public Task Commit()
        {
            return _unitOfWork.Commit();
        }
    }
}
