using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Culture.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : Controller
    {

        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IEventService _eventService;
        private readonly INotificationService _notificationService;

        public CommentsController(
            IUserService userService,
            ICommentService commentService,
            IEventService eventService,
            INotificationService notificationService)
        {
            _userService = userService;
            _commentService = commentService;
            _eventService = eventService;
            _notificationService = notificationService;
        }
        [HttpPost("create")]
        public async Task<JsonResult> CreateEventComment([FromBody]CommentViewModel comment)
        {

            try
            {
                var user = await _userService.GetUserByName("lukasz");
                var _comment = await _commentService.CreateCommentAsync(comment.Content, comment.EventId, user.Id);
                var eventOrganizer = await _userService.GetUserByEventId();
                var notificationTargets = new List<Guid>() { comment.}
                _notificationService.CreateNotificationsAsync
                await _commentService.Commit();

                return Json(_comment);

            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }
        [HttpGet("get")]
        public async Task<JsonResult> GetEventComments([FromQuery]int id,int skip=0,int take=5)
        {
            try
            {
                var comment = await _commentService.GetEventCommentsAsync(id,skip,take);

                return Json(comment);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }

        }
        [HttpPut("edit")]
        public async Task<JsonResult> EditEventComment([FromBody]CommentViewModel comment)
        {
            try
            {
                var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
                var _comment = await _commentService.EditCommentAsync(comment, user.Id);
                await _commentService.Commit();

                return Json(_comment);

            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteEventComment([FromQuery] int commentId)
        {
            try
            {
                var user = await _userService.GetUserByName(HttpContext.User.Identity.Name);
                var userRoles = await _userService.GetUserRoles(user);
                await _commentService.DeleteComment(commentId, user.Id, userRoles);
                await _commentService.Commit();
                return Ok();

            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

    }
}