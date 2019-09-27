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
            _unitOfWork = unitOfWork;
        }

        public async Task<Event> CreateEventAsync(EventViewModel eventViewModel, string imagePath, Guid userId)
        {
            var eventDate = convertDate(eventViewModel.EventDate, eventViewModel.EventTime);
            var urlSlug = $"{eventViewModel.Name.ToLower().Replace(' ', '-')}-{Guid.NewGuid().ToString()}";
            var eventt = new Event()
            {
                UrlSlug = urlSlug,
                Category = eventViewModel.Category,
                CityName = eventViewModel.CityName,
                Content = eventViewModel.Content,
                CreatedById = userId,
                ImagePath = imagePath,
                Name = eventViewModel.Name,
                StreetName = eventViewModel.StreetName,
                Price = eventViewModel.Price,
                CreationDate = DateTime.Now,
                TakesPlaceDate = eventDate,
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

        public async Task DeleteEvent(int id, Guid userId, IList<string> userRoles)
        {
            var _event = await _unitOfWork.EventRepository.GetEventAsync(id);
            if (_event.CreatedById == userId || userRoles.Contains("Admin"))
            {
                _unitOfWork.EventRepository.DeleteEvent(_event);
            }
            return;
        }
        public async Task<EventReactionsWAuthorDto> GetEventReactionsWAuthor(int id)
        {
            var _event = await _unitOfWork.EventRepository.GetEventAsync(id);
            var eventReactions = _event.Reactions.GroupBy(x => x.Type).Select(x => new EventReactionDto()
            {
                ReactionType = x.Key.ToString().ToLower(),
                Count = x.Count(),
            }).OrderByDescending(x => x.Count);
            return new EventReactionsWAuthorDto()
            {
                Id = _event.CreatedById,
                Reactions = eventReactions
            };

        }

        public async Task<EventDetailsDto> GetEventDetailsBySlugAsync(string slug, Guid userId, int size = 5)
        {
            var eventDetails = await _unitOfWork.EventRepository.GetEventDetailsBySlugAsync(slug);

            var isInCalendar = eventDetails.EventsInCalendar
                .FirstOrDefault(x => x.Calendar.BelongsToId == userId && x.EventId == eventDetails.Id)
                != null ? true : false;

            return new EventDetailsDto(eventDetails)
            {
                IsInCalendar = isInCalendar,
            };
        }

        public Task Commit()
        {
            return _unitOfWork.Commit();
        }

        public async Task<EventsPreviewWithLoadDto> GetEventPreviewList(Guid userId, int page = 0, int size = 5, string category = null, string query = null)
        {
            var eventList = await _unitOfWork.EventRepository.GetEventPreviewList(page, size, category, query);

            var canLoadMore = eventList.Count() > 5 ? true : false;
            eventList = canLoadMore == true ? eventList.Take(size) : eventList;

            var EventsPreviewDtoList = new EventsPreviewWithLoadDto();

            foreach (var eventEntity in eventList)
            {
                var userReaction = await _unitOfWork.EventReactionRepository.GetUserReactionAsync(userId, eventEntity.Id);
                var reactions = await _unitOfWork.EventReactionRepository.GetEventReactions(eventEntity.Id);

                var groupedReactions = reactions
                    .GroupBy(x => x.Type)
                    .Select(x => new EventReactionDto()
                    {
                        Count = x.Count(),
                        ReactionType = x.Key.ToString().ToLower()
                    })
                    .OrderByDescending(y => y.Count);

                var commentsCount = await _unitOfWork.CommentRepository.GetCommentCountAsync(eventEntity.Id);
                var comments = await _unitOfWork.CommentRepository.GetEventCommentsAsync(eventEntity.Id, 0, 5);
                var commentsDto = comments.Select(x => new CommentDto(x));

                var moreCommentsDto = new MoreCommentsDto(commentsDto)
                {
                    TotalCount = commentsCount
                };

                EventsPreviewDtoList.Events.Add(new EventsPreviewDto(eventEntity, moreCommentsDto)
                {
                    CurrentReaction = userReaction?.Type.ToString().ToLower(),
                    CommentsCount = commentsCount,
                    Reactions = groupedReactions,
                    ReactionsCount = reactions.Count(),

                });
            }
            return EventsPreviewDtoList;
        }
        private DateTime convertDate(string[] date, string time)
        {
            var intDate = Array.ConvertAll(date, Int32.Parse);
            var timeArray = Array.ConvertAll(time.Split(':'), Int32.Parse);
            return new DateTime(intDate[0], intDate[1], intDate[2], timeArray[0], timeArray[1], 0);
        }
    }
}
