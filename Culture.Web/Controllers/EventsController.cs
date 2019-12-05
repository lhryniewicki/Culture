using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Culture.Utilities.ExtensionMethods;
using Culture.Utilities.Enums;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Culture.Contracts.Facades;

namespace Culture.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventsFacade _eventsFacade;

        public EventsController(IEventsFacade eventsFacade)
        {
            _eventsFacade = eventsFacade;
        }

        [Authorize(Roles = "User,Admin")]
		[HttpPost("create")]
		public async Task<JsonResult> CreateEvent([FromForm]EventViewModel eventViewModel)
		{
			try
			{
                var eventModel = await _eventsFacade.CreateEvent(eventViewModel);

                return Json(eventModel);
			}
			catch (Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message+e.InnerException);
			}
		}
        [Authorize(Roles ="User,Admin")]
        [AllowAnonymous]
        [HttpGet("get/details/{slug}")]
		public async Task<JsonResult> GetEvent([FromRoute]string slug)
		{
			try
			{
                var eventViewModel = await _eventsFacade.GetEvent(slug);

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
                var eventViewModel = await _eventsFacade.GetEventPreviewList(page, size, category, query);

                return Json(eventViewModel);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }

        }

        [Authorize(Roles = "User,Admin")]
        [HttpPut("edit")]
        public async Task<IActionResult> Edit([FromBody]EventViewModel eventViewModel)
        {
            try
            {
                await _eventsFacade.Edit(eventViewModel);

                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [HttpDelete("delete/{eventId}")]
        public async Task<IActionResult> DeleteEvent([FromRoute]int eventId)
        {
            try
            {
                await _eventsFacade.DeleteEvent(eventId);

                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [Authorize(Roles = "User,Admin")]
		[HttpPost("reaction")]
		public async Task<JsonResult> SetReaction(SetReactionViewModel reactionViewModel)
		{
			try
			{
                return Json(await _eventsFacade.SetReaction(reactionViewModel));
			}
			catch(Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message + e.InnerException);
			}
		}

        [Authorize(Roles = "User,Admin")]
        [HttpGet("geolocation")]
        public async Task<JsonResult> GetUserMap()
        {
            try
            {
                var mapCoords = await _eventsFacade.GetUserMap();

                return Json(mapCoords);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [AllowAnonymous]
        [HttpPost("get/allMap")]
        public async Task<JsonResult> GetAllMap(AllMapViewModel allMapViewModel)
        {
            try
            {
                var mapCoords = await _eventsFacade.GetAllMap(allMapViewModel.Query, allMapViewModel.Dates, allMapViewModel.Category);

                return Json(mapCoords);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [AllowAnonymous]
        [HttpPost("get/allCalendar")]
        public async Task<JsonResult> GetAllCalendar(AllMapViewModel allMapViewModel)
        {
            try
            {
                var mapCoords = await _eventsFacade.GetAllCalendar(allMapViewModel.Query, allMapViewModel.Dates, allMapViewModel.Category);

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
            try
            {
                var participants = await _eventsFacade.GetEventParticipants(eventId, query);

                return Json(participants);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [HttpGet("get/map/{eventId}")]
        public async Task<JsonResult> GetEventMap([FromRoute]int eventId)
        {
            try
            {
                var eventMap = await _eventsFacade.GetEventMap(eventId);

                return Json(eventMap);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }
    }
}
