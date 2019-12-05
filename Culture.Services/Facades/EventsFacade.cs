using Culture.Contracts.DTOs;
using Culture.Contracts.Facades;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using Culture.Utilities.Enums;
using Culture.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Implementation.Facades
{

    public class EventsFacade : IEventsFacade
    {
        private readonly IEventService _eventService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly INotificationService _notificationService;
        private readonly IReactionService _reactionService;
        private readonly IFileService _fileService;
        private readonly IEventReactionService _eventReactionService;
        private readonly IGeolocationService _geolocationService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EventsFacade(
            IEventService eventService,
            IUserService userService,
            ICommentService commentService,
            INotificationService notificationService,
            IReactionService reactionService,
            IFileService fileService,
            IEventReactionService eventReactionService,
            IGeolocationService geolocationService,
            IEmailService emailService,
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _eventService = eventService;
            _userService = userService;
            _commentService = commentService;
            _notificationService = notificationService;
            _reactionService = reactionService;
            _fileService = fileService;
            _eventReactionService = eventReactionService;
            _geolocationService = geolocationService;
            _emailService = emailService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Event> CreateEvent(EventViewModel eventViewModel)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var imagePath = await _fileService.UploadImage(eventViewModel.Image);

            var geometry = await _geolocationService.Localize(eventViewModel.CityName, eventViewModel.StreetName);

            var eventModel = await _eventService.CreateEventAsync(eventViewModel, imagePath, Guid.Parse(userId), geometry);

            await _eventService.Commit();

            return eventModel;
        }

        public async Task DeleteEvent(int eventId)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);
            var userRole = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.Role);

            var user = await _userService.GetUserById(userId);

            var eventParticipants = await _userService.GetEventParticipants(eventId);

            var _event = await _eventService.GetEventAsync(eventId);
            await _eventService.DeleteEvent(eventId, user.Id, userRole);

            await _notificationService.CreateNotificationsAsync(
                $"Wydarzenie zostało usunięte: {_event.Name}!", eventParticipants.Select(x => x.Id), _event.Id, _event.UrlSlug);

            var emailContent = $"Wydarzenie zostało usunięte: {_event.Name}' ";
            await _emailService.SendEmail(emailContent, eventParticipants);

            await _eventService.Commit();

        }

        public async Task Edit(EventViewModel eventViewModel)
        {

            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var userRole = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.Role);

            var eventEdit = await _eventService.EditEvent(eventViewModel, Guid.Parse(userId), userRole);

            var eventParticipants = await _userService.GetEventParticipants(eventViewModel.Id);

            if (eventEdit.NeedsGeolocation)
            {
                var geometry = await _geolocationService.Localize(eventViewModel.CityName, eventViewModel.StreetName);
                eventEdit.EditedEvent.Longitude = geometry?.Longtitute;
                eventEdit.EditedEvent.Latitude = geometry?.Latitute;
            }
                
            await _notificationService.CreateNotificationsAsync(
                $"Wydarzenie zostało zmienione: {eventEdit?.EditedEvent.Name}! Sprawdz jego szczegóły", eventParticipants.Select(x => x.Id), eventViewModel.Id, eventEdit.EditedEvent.UrlSlug);

            var emailContent = $"Wydarzenie zostało zmienione: <a href='{_configuration["Values:MessageDomain"]}/wydarzenie/szczegoly/{eventEdit?.EditedEvent.UrlSlug}'> Sprawdz jego szczegóły </a>";
            await _emailService.SendEmail(emailContent, eventParticipants);

            await _eventService.Commit();
        }

        public async Task<IEnumerable<MapGeometryDto>> GetAllMap(string query = null, IEnumerable<string[]> dates = null, string category = null)
        {
            return await _geolocationService.GetAllMap(query,dates,category);
        }

        public async Task<IEnumerable<DateTime>> GetAllCalendar(string query = null, IEnumerable<string[]> dates = null, string category = null)
        {
            return await _eventService.GetAllCalendar(query, dates, category);
        }

        public async Task<EventDetailsViewModel> GetEvent(string slug)
        {
            Guid userId;
            var res = Guid.TryParse(_httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti), out userId);
            userId = res == false ? Guid.Empty : userId;

            var eventDto = await _eventService.GetEventDetailsBySlugAsync(slug, userId);
            var recommendedEvents = await _eventService.GetRecommendedEvents(eventDto.Id);

            var userConfig = await _userService.GetUserConfiguration(userId);
            var take = userConfig?.EventsDisplayAmount ?? 5;

            var commentsDto = await _commentService.GetEventCommentsAsync(eventDto.Id, 0, take);
            var reactions = await _eventReactionService.GetReactions(eventDto.Id, userId);

            var isUserAttending = await _userService.IsUserSigned(userId, eventDto.Id);


            return new EventDetailsViewModel(eventDto, commentsDto, reactions, isUserAttending, recommendedEvents);
        }

        public async Task<IEnumerable<MapGeometryDto>> GetEventMap(int eventId)
        {
            return await _geolocationService.GetEventMap(eventId);
        }

        public async Task<IEnumerable<ParticipantDto>> GetEventParticipants(int eventId, string query = null)
        {
            return await _eventService.GetEventParticipants(eventId, query);
        }

        public async Task<EventPreviewListViewModel> GetEventPreviewList(int page = 0, int size = 5, string category = null, string query = null)
        {
            Guid userId;
            var res = Guid.TryParse(_httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti), out userId);
            userId = res == false ? Guid.Empty : userId;

            var userConfig = await _userService.GetUserConfiguration(userId);
            var sizeEvents = userConfig?.EventsDisplayAmount ?? 5;
            var sizeComments = userConfig?.CommentsDisplayAmount ?? 5;

            var eventList = await _eventService.GetEventPreviewList(userId, page, sizeEvents, sizeComments, category, query);

            return new EventPreviewListViewModel(eventList);

        }

        public async Task<IEnumerable<MapGeometryDto>> GetUserMap()
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            return await _geolocationService.GetMap(Guid.Parse(userId));

        }

        public async Task<SortedReactionsViewModel> SetReaction(SetReactionViewModel reactionViewModel)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var user = await _userService.GetUserById(userId); var userRoles = await _userService.GetUserRoles(user);

            var hasPreviouslyReacted = await _reactionService.SetReaction(reactionViewModel, user.Id);

            var _newEventReactions = await _eventService.GetEventReactionsWAuthor(reactionViewModel.EventId);

            var targetList = new List<Guid>() { _newEventReactions.Id };

            if (!hasPreviouslyReacted)
            {
                await _notificationService.CreateNotificationsAsync($"{user.UserName} zareagował na twoje wydarzenie! {_newEventReactions.EventName}" +
                    $"{reactionViewModel.ReactionType}", targetList, reactionViewModel.EventId, _newEventReactions.EventSlug);
            }
            await _reactionService.Commit();

            return new SortedReactionsViewModel(_newEventReactions.Reactions);
        }
    }
}
