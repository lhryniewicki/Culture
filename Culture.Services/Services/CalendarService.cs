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
        public  Task SignUserToEvent(int eventId,AppUser user)
        {
            var userEvent = new UserInEvent()
            {
                EventId = eventId,
                UserId = user.Id
            };
           return _unitOfWork.UserInEventRepository.SignUserToEvent(userEvent);
        }
        public Task AddToCalendar(int eventId,AppUser user)
        {
            var eventCalendar = new EventInCalendar()
            {
                CalendarId = user.CalendarId,
                EventId = eventId
            };
            return _unitOfWork.EventInCalendarRepository.SignToCalendar(eventCalendar);
        }
        public async Task RemoveEventFromCalendar(int eventId, Guid userId)
        {
             await _unitOfWork.EventInCalendarRepository.RemoveFromCalendar(eventId, userId);
        }
        public Task Commit()
        {
            return _unitOfWork.Commit();
        }

    }
}
