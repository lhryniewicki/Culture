using Culture.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
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
		Task<Event> GetEventWithReactions(int id);
        Task<IEnumerable<AppUser>> GetParticipants(int eventId, string query);
        void DeleteEvent(Event _event);
    }
}
