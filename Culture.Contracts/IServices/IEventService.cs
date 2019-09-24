using Culture.Contracts.DTOs;
using Culture.Contracts.ViewModels;
using Culture.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
	public interface IEventService
	{
		Task<Event> CreateEventAsync(EventViewModel eventViewModel,string imagePath, Guid id);
        Task<Event> GetEventAsync(int id);
        Task<EventReactionsWAuthorDto> GetEventsReactions(int id);
        Task<IEnumerable<EventsPreviewDto>> GetEventPreviewList(IEnumerable<EventReaction> eventReactions,int page,int size, string category);
        Task<EventDetailsDto> GetEventDetailsBySlugAsync(string slug, IEnumerable<EventReaction> eventReactions, int size=5);
        Task<Event> EditEvent(EventViewModel eventViewModel, Guid id);
        Task DeleteEvent(int id, Guid userId, IList<string> userRoles);
        Task Commit();
    }
}
