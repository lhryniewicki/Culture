using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Culture.DataAccess.Context;
using Culture.Models;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Newtonsoft.Json;
//to do iterator na more comments more events
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

        public EventsController(
			IEventService eventService,
			IUserService userService,
			ICommentService commentService,
			INotificationService notificationService,
			IReactionService reactionService,
            IFileService fileService,
            IEventReactionService eventReactionService)
        {
			_eventService = eventService;
			_userService = userService;
			_commentService = commentService;
			_notificationService = notificationService;
			_reactionService = reactionService;
            _fileService = fileService;
            _eventReactionService = eventReactionService;
        }

		[HttpPost("create")]
		public async Task<JsonResult> CreateEvent([FromForm]EventViewModel eventViewModel)
		{
			try
			{
				var user =  await _userService.GetUserByName("maciek");

                if (user == null)
                {
                    Response.StatusCode = 401;
                    return Json("User not found in database");
                }

                var imagePath = await _fileService.UploadImage(eventViewModel.Image);

                var eventModel = await _eventService.CreateEventAsync(eventViewModel, imagePath, user.Id);

                await _eventService.Commit();

                return Json(eventModel);
			}
			catch (Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message+e.InnerException);
			}
		}

		[HttpGet("get/details/{slug}")]
		public async Task<JsonResult> GetEvent([FromRoute]string slug)
		{
			try
			{
                var user = await _userService.GetUserByName("maciek");
                var eventDto = await _eventService.GetEventDetailsBySlugAsync(slug, user.Id);

                var commentsDto = await  _commentService.GetEventCommentsAsync(eventDto.Id, 0, 5);
                var reactions = await _eventReactionService.GetReactions(eventDto.Id, user.Id);

                var isUserAttending = await _userService.IsUserSigned(user.Id, eventDto.Id);

                var eventViewModel = new EventDetailsViewModel(eventDto,commentsDto,reactions,isUserAttending);

				return Json(eventViewModel);
			}
			catch(Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message+e.InnerException);
			}
			
		}

        [HttpGet("get/preview")]
        public async Task<JsonResult> GetEventPreviewList(int page=0, int size=5, string category=null, string query = null)
        {
            try
            {
                var user = await _userService.GetUserByName("maciek");

                var eventList = await _eventService.GetEventPreviewList(user.Id,page, size,category,query);

                var eventViewModel = new EventPreviewListViewModel(eventList);

                return Json(eventViewModel);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }

        }

        [HttpPut("edit")]
        public async Task<JsonResult> Edit([FromBody]EventViewModel eventViewModel)
        {
            try
            {
                var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);

                var eventReq = _eventService.EditEvent(eventViewModel, user.Id);

				var eventParticipantsReq = _userService.GetEventParticipants(eventViewModel.Id);

                await Task.WhenAll(new Task[] { eventReq, eventParticipantsReq });

                var _event = await eventReq;
                var eventParticipants = await eventParticipantsReq;

                await _notificationService.CreateNotificationsAsync(
					$"Wydarzenie zostało zmienione: {_event.Name}! Sprawdz jego szczegóły",eventParticipants, eventViewModel.Id);

                await _eventService.Commit();

                return Json(_event);

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
                var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
                var userRoles = await _userService.GetUserRoles(user);
				var eventParticipants = await _userService.GetEventParticipants(eventId);

				var _event = await _eventService.GetEventAsync(eventId);
                await _eventService.DeleteEvent(eventId, user.Id, userRoles);

				await _notificationService.CreateNotificationsAsync(
					$"Wydarzenie zostało usunięte: {_event.Name}!", eventParticipants, _event.Id);

				await _eventService.Commit();

                return Ok();

            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

		[HttpPost("reaction")]
		public async Task<JsonResult> SetReaction(SetReactionViewModel reactionViewModel)
		{
			try
			{
				var user = await _userService.GetUserByName("maciek");

				if (user.Id != reactionViewModel.UserId)
                {
                    Response.StatusCode = 401;
                    return  Json("You can't set somebody's reaction");
                }

                await _reactionService.SetReaction(reactionViewModel);

				var _newEventReactions = await _eventService.GetEventReactionsWAuthor(reactionViewModel.EventId);

				var targetList = new List<Guid>() { _newEventReactions.Id};

				await _notificationService.CreateNotificationsAsync($"{user.UserName} zareagował na twoje wydarzenie!" +
					$"{reactionViewModel.ReactionType}", targetList, reactionViewModel.EventId);

				await _reactionService.Commit();

                return Json(new SortedReactionsViewModel(_newEventReactions.Reactions));
			}
			catch(Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message + e.InnerException);
			}
		}
	}
}
