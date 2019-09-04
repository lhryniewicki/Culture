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

		public EventsController(
			IEventService eventService,
			IUserService userService,
			ICommentService commentService,
			INotificationService notificationService,
			IReactionService reactionService)
        {
			_eventService = eventService;
			_userService = userService;
			_commentService = commentService;
			_notificationService = notificationService;
			_reactionService = reactionService;
		}
		[HttpPost("create")]
		public async Task<JsonResult> CreateEvent([FromBody]EventViewModel eventt)
		{
			try
			{

				var user = await _userService.GetUserByName("lukasz");
				var _event = await _eventService.CreateEventAsync(eventt, user.Id);
                await _eventService.Commit();

                return Json(_event);

			}
			catch (Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message+e.InnerException);
			}
		}

		[HttpGet("get")]
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
        [HttpPut("edit")]
        public async Task<JsonResult> EditEvent([FromBody]EventViewModel eventViewModel)
        {
            try
            {
                var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
                var _event = await _eventService.EditEvent(eventViewModel, user.Id);
				var eventParticipants = await _userService.GetEventParticipants(eventViewModel.Id);

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
					$"Wydarzenie zostało usunięte: {_event.Name}! Sprawdz jego szczegóły!", eventParticipants, _event.Id);

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
		public async Task<int> SetReaction(SetReactionViewModel reactionViewModel)
		{
			try
			{

				var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
				if (user.Id != reactionViewModel.UserId) throw new Exception("User does not exist");

				await _reactionService.SetReaction(reactionViewModel);

				var _event = await _eventService.GetEventAsync(reactionViewModel.EventId);
				var targetList = new List<Guid>() { _event.CreatedById };

				await _notificationService.CreateNotificationsAsync($"{user.UserName} zareagował na twoje wydarzenie!" +
					$"{reactionViewModel.ReactionType}", targetList, reactionViewModel.EventId);

				await _reactionService.Commit();

				return _event.Reactions
					.GroupBy(x => x.Type)
					.Sum(x => x.Count());
			}
			catch(Exception e)
			{
				Response.StatusCode = 500;
				return 0;
				//return Json(e.Message + e.InnerException);
			}



		}





	}
}
