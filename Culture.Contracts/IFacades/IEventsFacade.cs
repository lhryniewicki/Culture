using Culture.Contracts.DTOs;
using Culture.Contracts.ViewModels;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Culture.Contracts.Facades
{
    public interface IEventsFacade
    {
        Task<Event> CreateEvent(EventViewModel eventViewModel);
        Task<EventDetailsViewModel> GetEvent(string slug);
        Task<EventPreviewListViewModel> GetEventPreviewList(int page = 0, int size = 5, string category = null, string query = null);
        Task Edit(EventViewModel eventViewModel);
        Task DeleteEvent(int eventId);
        Task<SortedReactionsViewModel> SetReaction(SetReactionViewModel reactionViewModel);
        Task<IEnumerable<MapGeometryDto>> GetUserMap();
        Task<IEnumerable<ParticipantDto>> GetEventParticipants(int eventId, string query = null);
        Task<IEnumerable<MapGeometryDto>> GetEventMap(int eventId);
        Task<IEnumerable<MapGeometryDto>> GetAllMap(string query = null, IEnumerable<string[]> dates = null, string category = null);
        Task<IEnumerable<DateTime>> GetAllCalendar(string query = null, IEnumerable<string[]> dates = null, string category = null);

    }
}
