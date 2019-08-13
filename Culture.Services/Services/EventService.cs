using Culture.Contracts.IRepositories;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
	public class EventService : IEventService
	{
		private readonly IEventRepository _eventRepository;

		public EventService(
			IEventRepository eventRepository
			)
		{
			_eventRepository = eventRepository;
		}

		public async Task<Event> CreateEventAsync(EventViewModel eventViewModel,Guid userId)
		{
			var eventt = new Event()
			{
				Category = eventViewModel.Category,
				CityName = eventViewModel.CityName,
				Content = eventViewModel.Content,
				CreatedById = userId,
				Name = eventViewModel.Name,
				StreetName = eventViewModel.StreetName,
				Price = eventViewModel.Price
			};
			await _eventRepository.AddEventAsync(eventt);

			return eventt;

		}


		public Task<Event> GetEventDetailsAsync(int id)
		{
			return  _eventRepository.GetEventDetailsAsync(id);
		}

	}
}
