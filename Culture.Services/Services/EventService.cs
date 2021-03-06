﻿using Culture.Contracts;
using Culture.Contracts.DTOs;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Implementation.SignalR;
using Culture.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Culture.Services.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHubContext<EventHub> _hubContext;

        public EventService(
            IUnitOfWork unitOfWork,
            IHubContext<EventHub> hubContext
            )
        {
            _unitOfWork = unitOfWork;
            _hubContext = hubContext;
        }

        public async Task<Event> CreateEventAsync(EventViewModel eventViewModel, string imagePath, Guid userId, GeometryDto geometryDto)
        {
            var eventDate = convertDate(eventViewModel.EventDate, eventViewModel.EventTime);

            var urlSlug = $"{eventViewModel.Name.ToLower().Replace(' ', '-').Replace('/', '-').Replace(':','-').Replace(',','-').Replace('.','-')}-{Guid.NewGuid().ToString()}";

            var eventt = new Event()
            {
                Latitude = geometryDto?.Latitute,
                Longitude = geometryDto?.Longtitute,
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

        public async Task<EditEventDto> EditEvent(EventViewModel eventViewModel, Guid id, string userRole)
        {
            var _event = await GetEventAsync(eventViewModel.Id);

            var eventDate = convertDate(eventViewModel.EventDate, eventViewModel.EventTime);

            if (_event.CreatedById != id && userRole != "Admin") return null;

            var needsGeolocation = !(_event.StreetName == eventViewModel.StreetName && _event.CityName == eventViewModel.CityName);

            _event.Name = eventViewModel.Name;
            _event.Price = eventViewModel.Price;
            _event.StreetName = eventViewModel.StreetName;
            _event.CityName = eventViewModel.CityName;
            _event.Content = eventViewModel.Content;
            _event.Category = eventViewModel.Category;
            _event.TakesPlaceDate = eventDate;


            return new EditEventDto()
            {
                EditedEvent = _event,
                NeedsGeolocation=needsGeolocation
            };


        }
        public Task<Event> GetEventAsync(int id)
        {
            return _unitOfWork.EventRepository.GetEventAsync(id);
        }

        public async Task DeleteEvent(int id, Guid userId, string userRole)
        {
            var _event = await _unitOfWork.EventRepository.GetEventAsync(id);
            if (_event.CreatedById == userId || userRole == "Admin")
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
                Reactions = eventReactions,
                EventName = _event.Name,
                EventSlug = _event.UrlSlug
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

        public async Task<EventsPreviewWithLoadDto> GetEventPreviewList(Guid userId, int page = 0, int sizeEvents = 5, int sizeComments=5, string category = null, string query = null)
        {
            var eventList = await _unitOfWork.EventRepository.GetEventPreviewList(page, sizeEvents, category, query);

            string connectionId = null;

            var found = EventHub.userIdConnectionId.TryGetValue(userId.ToString(), out connectionId);

            foreach (var e in eventList)
            {
                await _hubContext.Groups.AddToGroupAsync(connectionId, $"event-{e.Id}");
            }

            var userAvatarPath = await _unitOfWork.UserRepository.GetUserById(userId.ToString()).ContinueWith(x => x.Result?.AvatarPath);

            var canLoadMore = eventList.Count() > sizeEvents ? true : false;
            eventList = canLoadMore == true ? eventList.Take(sizeEvents) : eventList;

            var EventsPreviewDtoList = new EventsPreviewWithLoadDto()
            {
                CanLoadMore = canLoadMore,
                AvatarPath = userAvatarPath
            };

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
                var comments = await _unitOfWork.CommentRepository.GetEventCommentsAsync(eventEntity.Id, 0, sizeComments);
                var commentsDto = comments.Select(x => new CommentDto(x));

                var moreCommentsDto = new MoreCommentsDto(commentsDto, sizeComments)
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

        public async Task<IEnumerable<RecommendedEventDto>> GetRecommendedEvents(int eventId)
        {
            var eventData = await _unitOfWork.EventRepository.GetEventAsync(eventId);

            var recommendedEvents = await _unitOfWork.EventRepository.GetRecommendedEvents(eventData);

            return recommendedEvents.Select(x => new RecommendedEventDto(x));
        }

        public async Task<IEnumerable<ParticipantDto>> GetEventParticipants(int eventId,string query)
        {
            var participants = await _unitOfWork.EventRepository.GetParticipants(eventId, query);

            return participants.Select(x => new ParticipantDto()
            {
                UserId = x.Id,
                Username = x.UserName
            }).ToList();
        }

        public Task<IEnumerable<DateTime>> GetAllCalendar(string query = null, IEnumerable<string[]> dates = null, string category = null)
        {
            return _unitOfWork.EventRepository.GetAllCalendar(query,dates,category);
        }
    }
}
