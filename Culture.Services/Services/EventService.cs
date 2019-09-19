using Culture.Contracts;
using Culture.Contracts.DTOs;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

		public async Task<Event> CreateEventAsync(EventViewModel eventViewModel,string imagePath,Guid userId)
		{
            var eventDate = convertDate(eventViewModel.EventDate,eventViewModel.EventTime);

			var eventt = new Event()
			{
				Category = eventViewModel.Category,
				CityName = eventViewModel.CityName,
				Content = eventViewModel.Content,
				CreatedById = userId,
                ImagePath= imagePath,
				Name = eventViewModel.Name,
				StreetName = eventViewModel.StreetName,
				Price = eventViewModel.Price,
                CreationDate= DateTime.Now,
                TakesPlaceDate=eventDate
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
        public async Task<IEnumerable<EventsPreviewDto>> GetEventPreviewList(int page=0, string category=null)
        {
          
          var eventList = await _unitOfWork.EventRepository.GetEventPreviewList(page, category);


            return eventList.Select(x => new EventsPreviewDto()
            {
                Comments = x.Comments,
                CreatedBy = x.CreatedBy.UserName,
                CreationDate = x.CreationDate,
                Image = x.ImagePath,
                Reactions = x.Reactions,
                Name = x.Name,
                CommentsCount = x.Comments.Count,
                ReactionsCount = x.Reactions.Count,
                ShortContent = x.Content.Substring(0, x.Content.Length > 255 ? 255 : x.Content.Length),
                Id=x.Id
            });
        }
        private DateTime convertDate(string[] date,string time)
        {
            var intDate = Array.ConvertAll(date, Int32.Parse);
            var timeArray = Array.ConvertAll(time.Split(':'), Int32.Parse);
            return new DateTime(intDate[0], intDate[1], intDate[2], timeArray[0], timeArray[1],0);
        }

    }
}
