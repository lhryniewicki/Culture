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

        public EventsController(
			IEventService eventService,
			IUserService userService,
			ICommentService commentService,
			INotificationService notificationService,
			IReactionService reactionService,
            IFileService fileService)
        {
			_eventService = eventService;
			_userService = userService;
			_commentService = commentService;
			_notificationService = notificationService;
			_reactionService = reactionService;
            _fileService = fileService;
        }
		[HttpPost("create")]
		public async Task<JsonResult> CreateEvent([FromForm]EventViewModel eventt)
		{
			try
			{

				var user =  await _userService.GetUserByName("maciek");

                if (user == null)
                {
                    Response.StatusCode = 401;
                    return Json("User not found in database");
                }

                var imagePath = await _fileService.UploadImage(eventt.Image);

                var _event = await _eventService.CreateEventAsync(eventt,imagePath, user.Id);


                await _eventService.Commit();

                return Json(_event);

			}
			catch (Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message+e.InnerException);
			}
		}

		[HttpGet("/get/details")]
		public async Task<JsonResult> GetEvent(int id)
		{
			try
			{
                var _event = await _eventService.GetEventDetailsAsync(id);

				return Json(_event);
			}
			catch(Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message+e.InnerException);
			}
			
		}
        [HttpGet("get")]
        public async Task<JsonResult> GetEventPreviewList(int page=0,int size=5,string category=null)
        {
            try
            {
                var user = await _userService.GetUserByName("maciek");
                var _eventList = await _eventService.GetEventPreviewList(user.EventReactions,page, size,category);
                var _event = new EventPreviewListViewModel()
                {
                    Events = _eventList
                };
                return Json(_event);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }

        }
        [HttpPut("edit")]
        public async Task<JsonResult> EditEvent([FromBody]EventViewModel eventViewModel)
        {
            try
            {
                var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
                var _eventReq = _eventService.EditEvent(eventViewModel, user.Id);
				var eventParticipantsReq = _userService.GetEventParticipants(eventViewModel.Id);
                var _event = await _eventReq;
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

				var _eventReactions = await _eventService.GetEventsReactions(reactionViewModel.EventId);
				var targetList = new List<Guid>() { _eventReactions.Id};

				await _notificationService.CreateNotificationsAsync($"{user.UserName} zareagował na twoje wydarzenie!" +
					$"{reactionViewModel.ReactionType}", targetList, reactionViewModel.EventId);


				await _reactionService.Commit();

                return Json(new SortedReactionsViewModel(_eventReactions.Reactions));

                
			}
			catch(Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message + e.InnerException);
			}



		}





	}
}
