using Culture.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
    public interface IEventRepository
	{
        Task<IEnumerable<Event>> GetEventPreviewList(int page,int size, string category, string query = null);
		Task<Event> GetEventDetailsBySlugAsync(string slug);
        Task<IEnumerable<Event>> GetRecommendedEvents(Event queryEvent, int skip = 0, int take = 3);
        Task CreateEventAsync(Event eventt);
        Task<Event> GetEventAsync(int id);
        Task<IEnumerable<Event>> GetAllEvents(string query = null, IEnumerable<string[]> dates = null, string category = null);
        Task<Event> GetEventWithReactions(int id);
        Task<IEnumerable<AppUser>> GetParticipants(int eventId, string query);
        Task<IEnumerable<DateTime>> GetAllCalendar(string query, IEnumerable<string[]> dates, string category );
        void DeleteEvent(Event _event);
    }
}
