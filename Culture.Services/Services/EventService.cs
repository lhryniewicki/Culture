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
			await _unitOfWork.EventRepository.CreateEventAsync(eventt);

            return eventt;

		}

        public async Task<Event> EditEvent(EventViewModel eventViewModel, Guid id)
        {
           
               if (eventViewModel.AuthorId != id) return null;

            var _event = await GetEventAsync(eventViewModel.Id);

            _event.Name = eventViewModel.Name;
            _event.Price = eventViewModel.Price;
            _event.StreetName = eventViewModel.StreetName;
            _event.Content = eventViewModel.Content;
            _event.Category = eventViewModel.Category;
            _event.CityName = eventViewModel.CityName;


            return _event;

            
        }
        public Task<Event> GetEventAsync(int id)
        {
            return _unitOfWork.EventRepository.GetEventAsync(id);
        }

        public async Task DeleteEvent(int id,Guid userId,IList<string> userRoles)
        {
            var _event = await _unitOfWork.EventRepository.GetEventAsync(id);
            if (_event.CreatedById== userId || userRoles.Contains("Admin"))
            {
                _unitOfWork.EventRepository.DeleteEvent(_event);
            }
            return;
        }

        public Task<Event> GetEventDetailsAsync(int id)
		{
            return _unitOfWork.EventRepository.GetEventDetailsAsync(id);
		}
        public Task Commit()
        {
            return _unitOfWork.Commit();
        }
    }
}
