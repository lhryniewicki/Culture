﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Culture.Utilities.ExtensionMethods;
using Culture.Utilities.Enums;

namespace Culture.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
		private readonly IUserService _userService;
		private readonly ICommentService _commentService;
		private readonly INotificationService _notificationService;
		private readonly IReactionService _reactionService;
        private readonly IFileService _fileService;
        private readonly IEventReactionService _eventReactionService;
        private readonly IGeolocationService _geolocationService;

        public EventsController(
			IEventService eventService,
			IUserService userService,
			ICommentService commentService,
			INotificationService notificationService,
			IReactionService reactionService,
            IFileService fileService,
            IEventReactionService eventReactionService,
            IGeolocationService geolocationService)
        {
			_eventService = eventService;
			_userService = userService;
			_commentService = commentService;
			_notificationService = notificationService;
			_reactionService = reactionService;
            _fileService = fileService;
            _eventReactionService = eventReactionService;
            _geolocationService = geolocationService;
        }

        [Authorize]
		[HttpPost("create")]
		public async Task<JsonResult> CreateEvent([FromForm]EventViewModel eventViewModel)
		{
			try
			{
                var userId = User.GetClaim(JwtTypes.jti);

                var imagePath = await _fileService.UploadImage(eventViewModel.Image);

                var geometry = await _geolocationService.Localize(eventViewModel.CityName, eventViewModel.StreetName);

                var eventModel = await _eventService.CreateEventAsync(eventViewModel, imagePath, Guid.Parse(userId), geometry);

                await _eventService.Commit();

                return Json(eventModel);
			}
			catch (Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message+e.InnerException);
			}
		}
        [Authorize]
        [AllowAnonymous]
        [HttpGet("get/details/{slug}")]
		public async Task<JsonResult> GetEvent([FromRoute]string slug)
		{
			try
			{
                Guid userId;
                var res = Guid.TryParse(User.GetClaim(JwtTypes.jti), out userId);
                userId = res == false ? Guid.Empty : userId;

                var eventDto = await _eventService.GetEventDetailsBySlugAsync(slug, userId);
                var recommendedEvents = await _eventService.GetRecommendedEvents(eventDto.Id);

                var commentsDto = await  _commentService.GetEventCommentsAsync(eventDto.Id, 0, 5);
                var reactions = await _eventReactionService.GetReactions(eventDto.Id, userId);

                var isUserAttending = await _userService.IsUserSigned(userId, eventDto.Id);


                var eventViewModel = new EventDetailsViewModel(eventDto,commentsDto,reactions,isUserAttending,recommendedEvents);

				return Json(eventViewModel);
			}
			catch(Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message+e.InnerException);
			}
			
		}
        [Authorize]
        [AllowAnonymous]
        [HttpGet("get/preview")]
        public async Task<JsonResult> GetEventPreviewList(int page=0, int size=5, string category=null, string query = null)
        {
            try
            {
                Guid userId;
                var res = Guid.TryParse(User.GetClaim(JwtTypes.jti),out userId);
                userId = res == false ? Guid.Empty : userId;

                var eventList = await _eventService.GetEventPreviewList(userId,page, size,category,query);

                var eventViewModel = new EventPreviewListViewModel(eventList);

                return Json(eventViewModel);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }

        }

        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody]EventViewModel eventViewModel)
        {
            try
            {
                var userId = User.GetClaim(JwtTypes.jti);

                var eventEdit = await _eventService.EditEvent(eventViewModel, Guid.Parse(userId));

				var eventParticipants = await _userService.GetEventParticipants(eventViewModel.Id);

                if (eventEdit.NeedsGeolocation)
                {
                    var geometry = await _geolocationService.Localize(eventViewModel.CityName, eventViewModel.StreetName);
                    eventEdit.EditedEvent.Longitude = geometry.Longtitute;
                    eventEdit.EditedEvent.Latitude = geometry.Latitute;
                }

                await _notificationService.CreateNotificationsAsync(
					$"Wydarzenie zostało zmienione: {eventEdit?.EditedEvent.Name}! Sprawdz jego szczegóły",eventParticipants, eventViewModel.Id, eventEdit.EditedEvent.UrlSlug);

                await _eventService.Commit();

                return Ok();

            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            try
            {
                var userId = User.GetClaim(JwtTypes.jti);

                var user = await _userService.GetUserById(userId); var userRoles = await _userService.GetUserRoles(user);

				var eventParticipants = await _userService.GetEventParticipants(eventId);

				var _event = await _eventService.GetEventAsync(eventId);
                await _eventService.DeleteEvent(eventId, user.Id, userRoles);

				await _notificationService.CreateNotificationsAsync(
					$"Wydarzenie zostało usunięte: {_event.Name}!", eventParticipants, _event.Id,_event.UrlSlug);

				await _eventService.Commit();

                return Ok();

            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [Authorize]
		[HttpPost("reaction")]
		public async Task<JsonResult> SetReaction(SetReactionViewModel reactionViewModel)
		{
			try
			{
                var userId = User.GetClaim(JwtTypes.jti);

                var user = await _userService.GetUserById(userId); var userRoles = await _userService.GetUserRoles(user);

                var hasPreviouslyReacted = await _reactionService.SetReaction(reactionViewModel,user.Id);

				var _newEventReactions = await _eventService.GetEventReactionsWAuthor(reactionViewModel.EventId);

				var targetList = new List<Guid>() { _newEventReactions.Id};

                if(!hasPreviouslyReacted)
				await _notificationService.CreateNotificationsAsync($"{user.UserName} zareagował na twoje wydarzenie! {_newEventReactions.EventName}" +
					$"{reactionViewModel.ReactionType}", targetList, reactionViewModel.EventId, _newEventReactions.EventSlug);

				await _reactionService.Commit();

                return Json(new SortedReactionsViewModel(_newEventReactions.Reactions));
			}
			catch(Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message + e.InnerException);
			}
		}

        [Authorize]
        [HttpGet("geolocation")]
        public async Task<JsonResult> GetUserMap()
        {
            try
            {
                var userId = User.GetClaim(JwtTypes.jti);

                var mapCoords = await _geolocationService.GetMap(Guid.Parse(userId));

                return Json(mapCoords);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [HttpGet("get/participants/{eventId}")]
        public async Task<JsonResult> GetEventParticipants([FromRoute]int eventId,string query = null)
        {
            var participants = await _eventService.GetEventParticipants(eventId,query);

            return Json(participants);
        }

        [HttpGet("get/map/{eventId}")]
        public async Task<JsonResult> GetEventMap([FromRoute]int eventId)
        {
            var eventMap = await _geolocationService.GetEventMap(eventId);

            return Json(eventMap);
        }
    }
}
