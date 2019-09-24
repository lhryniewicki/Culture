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
            var urlSlug = $"{eventViewModel.Name.ToLower().Replace(' ', '-')}-{Guid.NewGuid().ToString()}";
			var eventt = new Event()
			{
                UrlSlug=urlSlug,
				Category = eventViewModel.Category,
				CityName = eventViewModel.CityName,
				Content = eventViewModel.Content,
				CreatedById = userId,
                ImagePath= imagePath,
				Name = eventViewModel.Name,
				StreetName = eventViewModel.StreetName,
				Price = eventViewModel.Price,
                CreationDate= DateTime.Now,
                TakesPlaceDate=eventDate,
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
        public async Task<EventReactionsWAuthorDto> GetEventsReactions(int id)
        {
            var _event = await _unitOfWork.EventRepository.GetEventAsync(id);
            var eventReactions= _event.Reactions.GroupBy(x => x.Type).Select(x => new EventReactionDto()
            {
                ReactionType = x.Key.ToString().ToLower(),
                Count = x.Count(),
            }).OrderByDescending(x=>x.Count);
            return new EventReactionsWAuthorDto()
            {
                Id = _event.CreatedById,
                Reactions = eventReactions
            };

        }

        public async Task<EventDetailsDto> GetEventDetailsBySlugAsync(string slug,IEnumerable<EventReaction> eventReactions,int size=5)
		{
            var eventDetails = await _unitOfWork.EventRepository.GetEventDetailsBySlugAsync(slug);

            var totalCommentsCount = eventDetails.Comments.Count;
            var comments = eventDetails.Comments
                .OrderByDescending(x => x.CreationDate).Take(size + 1)
                .Select(y => new CommentDto(y));

            var canLoadMore = comments.Count() == (size + 1) ? true : false;
            comments = comments.Count() > 5 ? comments.Take(size) : comments;

            var reactions = eventDetails.Reactions
                .GroupBy(x => x.Type)
                .Select(x => new EventReactionDto()
                {
                    Count = x.Count(),
                    ReactionType = x.Key.ToString().ToLower()
                })
                .OrderByDescending(x => x.Count);
            var reactionsCount = eventDetails.Reactions.Count;
            var currentReactions = eventReactions
                .Where(x => x.EventId == eventDetails.Id)
                .Select(x => x.Type.ToString()
                .ToLower())
                .FirstOrDefault();

            return new EventDetailsDto(eventDetails)
            {
                CurrentReaction=currentReactions,
                CommentsCount=totalCommentsCount,
                CanLoadMore=canLoadMore,
                Comments=comments,
                Reactions=reactions,
                ReactionsCount=reactionsCount
            };
		}
        public Task Commit()
        {
            return _unitOfWork.Commit();
        }
        public async Task<IEnumerable<EventsPreviewDto>> GetEventPreviewList(IEnumerable<EventReaction> userReactions, int page=0,int size=5, string category=null)
        {
          
          var eventList = await _unitOfWork.EventRepository.GetEventPreviewList(page,size, category);

          return eventList.Select(x => new EventsPreviewDto(x,size,userReactions));
        }
        private DateTime convertDate(string[] date,string time)
        {
            var intDate = Array.ConvertAll(date, Int32.Parse);
            var timeArray = Array.ConvertAll(time.Split(':'), Int32.Parse);
            return new DateTime(intDate[0], intDate[1], intDate[2], timeArray[0], timeArray[1],0);
        }

    }
}
