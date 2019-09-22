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
        Task<IEnumerable<Event>> GetEventPreviewList(int page,int size, string category);
		Task<Event> GetEventDetailsAsync(int id);
		Task CreateEventAsync(Event eventt);
        Task<Event> GetEventAsync(int id);
		Task<Event> GetEventWithReactions(int id);
		void DeleteEvent(Event _event);
    }
}
