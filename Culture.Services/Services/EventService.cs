using Culture.Contracts;
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
		private readonly IUnitOfWork _unitOfWork;

		public EventService(
            IUnitOfWork unitOfWork
            )
		{
			_unitOfWork=unitOfWork;
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
			await _unitOfWork.EventRepository.AddEventAsync(eventt);
            await _unitOfWork.Commit();

            return eventt;

		}

        public async Task<Event> EditEvent(EventViewModel eventViewModel, Guid id)
        {
           
               if (eventViewModel.AuthorId != id) return null;

            var _event = await _unitOfWork.EventRepository.GetEventAsync(eventViewModel.Id);

            _event.Name = eventViewModel.Name;
            _event.Price = eventViewModel.Price;
            _event.StreetName = eventViewModel.StreetName;
            _event.Content = eventViewModel.Content;
            _event.Category = eventViewModel.Category;
            _event.CityName = eventViewModel.CityName;

            await _unitOfWork.Commit();

            return _event;

            
        }

        public Task<Event> GetEventDetailsAsync(int id)
		{
            return _unitOfWork.EventRepository.GetEventDetailsAsync(id);
		}

	}
}
