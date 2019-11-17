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
		Task<Event> CreateEventAsync(EventViewModel eventViewModel,string imagePath, Guid id, GeometryDto geometryDto);
        Task<Event> GetEventAsync(int id);
        Task<EventReactionsWAuthorDto> GetEventReactionsWAuthor(int id);
        Task<EventsPreviewWithLoadDto> GetEventPreviewList(Guid userId,int page,int sizeEvents, int sizeComments, string category, string query = null);
        Task<EventDetailsDto> GetEventDetailsBySlugAsync(string slug,Guid userId, int size=5);
        Task<IEnumerable<RecommendedEventDto>> GetRecommendedEvents(int eventId);
        Task<EditEventDto> EditEvent(EventViewModel eventViewModel, Guid id, string userRole);
        Task DeleteEvent(int id, Guid userId, string userRoles);
        Task<IEnumerable<ParticipantDto>> GetEventParticipants(int eventId, string query);
        Task Commit();
    }
}
