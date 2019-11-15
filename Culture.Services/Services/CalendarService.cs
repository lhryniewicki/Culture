using Culture.Contracts;
using Culture.Contracts.IServices;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public  Task SignUserToEvent(int eventId,Guid userId)
        {
            var userEvent = new UserInEvent()
            {
                EventId = eventId,
                UserId = userId
            };
           return _unitOfWork.UserInEventRepository.SignUserToEvent(userEvent);
        }

        public Task UnsignUserFromEvent(int eventId, Guid userId)
        {
            return _unitOfWork.UserInEventRepository.UnsignUserFromEvent(eventId, userId);
        }

        public Task AddToCalendar(int eventId,int calendarId)
        {
            var eventCalendar = new EventInCalendar()
            {
                CalendarId = calendarId,
                EventId = eventId
            };
            return _unitOfWork.EventInCalendarRepository.SignToCalendar(eventCalendar);
        }

        public async Task RemoveEventFromCalendar(int eventId, Guid userId)
        {
             await _unitOfWork.EventInCalendarRepository.RemoveFromCalendar(eventId, userId);
        }

        public async Task<IEnumerable<DateTime>> GetUserCalendarDays(Guid userId, string category, string query)
        {

            if (category == null && query == null)
            {
                var userWithCalendar = await _unitOfWork.UserRepository.GetUserByIdWithCalendar(userId);

                return userWithCalendar
                    .Calendar
                    .Events
                    .Select(x => x.Event.TakesPlaceDate)
                    .ToList();
            }
            return null;
        }

        public Task Commit()
        {
            return _unitOfWork.Commit();
        }

        public async Task<bool> CheckIfExists(int eventId, Guid userId)
        {
            var userCalendar = await _unitOfWork.UserRepository.GetUserByIdWithCalendar(userId);

            return userCalendar.Calendar.Events.Any(x=>x.EventId == eventId);
        }
    }
}
