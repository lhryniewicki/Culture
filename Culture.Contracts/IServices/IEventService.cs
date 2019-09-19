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
        Task<IEnumerable<EventsPreviewDto>> GetEventPreviewList(int page, string category);
        Task<Event> GetEventDetailsAsync(int id);
        Task<Event> EditEvent(EventViewModel eventViewModel, Guid id);
        Task DeleteEvent(int id, Guid userId, IList<string> userRoles);
        Task Commit();
    }
}
