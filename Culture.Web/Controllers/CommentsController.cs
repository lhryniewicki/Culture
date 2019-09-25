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
        [HttpPost]
        public async Task<JsonResult> Create([FromBody]CommentViewModel commentViewModel)
        {

            try
            {
                var userReq = _userService.GetUserById("2acb229f-73ab-4202-1102-08d740193056");
                var eventReq =  _eventService.GetEventAsync(commentViewModel.EventId);

                var eventModel = await eventReq;
                var notificationTargets = new List<Guid>() { eventModel.CreatedById };

                var user = await userReq;

                var commentDtoReq =  _commentService.CreateCommentAsync(commentViewModel.Content, commentViewModel.EventId, user.Id,user.UserName);
                var notificationReq =  _notificationService.CreateNotificationsAsync($"Twoje wydarzenie zostało skomentowane: {eventModel.Name}", notificationTargets, eventModel.Id);

                await Task.WhenAll(new Task[] { commentDtoReq, notificationReq });
                var commentDto = await commentDtoReq;

                await _commentService.Commit();

                return Json(commentDto);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }
        [HttpGet("get")]
        public async Task<JsonResult> GetEventComments(int eventId,int page=0,int take=5)
        {
            try
            {
                var commentBatchDto = await _commentService.GetEventCommentsAsync(eventId,page,take);

                var commentVM = new CommentsListViewModel(commentBatchDto);

                return Json(commentVM);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }

        }
        [HttpPut("edit")]
        public async Task<JsonResult> EditEventComment([FromBody]EditCommentViewModel comment)
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
        public async Task<IActionResult> DeleteEventComment(int commentId)
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