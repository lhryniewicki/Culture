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

namespace Culture.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;
		private readonly IUserService _userService;
		private readonly ICommentService _commentService;

		public EventsController(
			IEventService eventService,
			IUserService userService,
			ICommentService commentService)
        {
			_eventService = eventService;
			_userService = userService;
			_commentService = commentService;
		}
		[HttpPost("event/create")]
		public async Task<JsonResult> CreateEvent([FromBody]EventViewModel eventt)
		{
			try
			{

				var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
				var _event = await _eventService.CreateEventAsync(eventt, user.Id);
				return Json(_event);

			}
			catch (Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message);
			}
		}
		[HttpPost("comment/create")]
		public async Task<JsonResult> CreateEventComment([FromBody]CommentViewModel comment)
		{
			try
			{
				var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
				var _comment = await _commentService.CreateCommentAsync(comment.Content,comment.EventId, user.Id);

				return Json(_comment);

			}
			catch (Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message);
			}
		}
		[HttpPut("comment/edit")]
		public async Task<JsonResult> EditEventComment([FromBody]CommentViewModel comment)
		{
			try
			{
				var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
				var _comment = await _commentService.EditCommentAsync(comment, user.Id);

				return Json(_comment);

			}
			catch (Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message);
			}
		}
		[HttpGet("event/get")]
		public async Task<JsonResult> GetEvent([FromQuery]int id)
		{
			try
			{
				var _event = await _eventService.GetEventDetailsAsync(id);

				return Json(_event);
			}
			catch(Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message);
			}
			
		}




	}
}
