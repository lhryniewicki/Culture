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
		Task<Event> CreateEventAsync(EventViewModel eventViewModel, Guid id);
		Task<Event> GetEventDetailsAsync(int id);

	}
}
