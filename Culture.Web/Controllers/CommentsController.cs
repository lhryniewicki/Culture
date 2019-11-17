using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using Culture.Utilities.Enums;
using Culture.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;

        public CommentsController(
            IUserService userService,
            ICommentService commentService,
            IEventService eventService,
            INotificationService notificationService,
            IFileService fileService,
            IConfiguration configuration,
            IEmailService emailService)
        {
            _userService = userService;
            _commentService = commentService;
            _eventService = eventService;
            _notificationService = notificationService;
            _fileService = fileService;
            _configuration = configuration;
            _emailService = emailService;
        }

        [Authorize]
        [HttpPost("create")]
        public async Task<JsonResult> CreateComment([FromForm]CommentViewModel commentViewModel)
        {
            try
            {
                var userId = User.GetClaim(JwtTypes.jti);

                var user = await _userService.GetUserById(userId);

                var eventModel =  await _eventService.GetEventAsync(commentViewModel.EventId);

                var notificationTargets = new List<Guid>() { eventModel.CreatedById };

                string imagePath = null;

                if(commentViewModel.Image != null ) imagePath = await _fileService.UploadImage(commentViewModel.Image); ;

                var commentDto = await  _commentService.CreateCommentAsync(commentViewModel.Content, commentViewModel.EventId, user.Id, user.UserName, imagePath);
                var notification = await  _notificationService.CreateNotificationsAsync($"Twoje wydarzenie zostało skomentowane: {eventModel.Name}", notificationTargets, eventModel.Id, eventModel.UrlSlug);

                var emailTarget = await _userService.GetUserById(eventModel.CreatedById.ToString());
                var emailContent = $"Twoje wydarzenie zostało skomentowane:  <a href='{_configuration["Values: MessageDomain"]}/wydarzenie/szczegoly/{eventModel.UrlSlug}'> Sprawdz szczegóły! </a>";

                await _emailService.SendEmail(emailContent, new List<AppUser>() { emailTarget });

                await _commentService.Commit();

                return Json(commentDto);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [Authorize]
        [AllowAnonymous]
        [HttpGet("get")]
        public async Task<JsonResult> GetEventComments(int eventId,int page=0,int take=5)
        {
            try
            {
                Guid userId;
                var res = Guid.TryParse(User.GetClaim(JwtTypes.jti), out userId);
                userId = res == false ? Guid.Empty : userId;

                var userConfig = await _userService.GetUserConfiguration(userId);
                take = userConfig?.EventsDisplayAmount ?? 5;

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

        [Authorize]
        [HttpPut("edit")]
        public async Task<IActionResult> EditEventComment([FromBody]EditCommentViewModel comment)
        {
            try
            {
                var userId = User.GetClaim(JwtTypes.jti);
                var userRole = User.GetClaim(JwtTypes.Role);

                var user = await _userService.GetUserById(userId);


                var _comment = await _commentService.EditCommentAsync(comment, Guid.Parse(userId), userRole);

                if (_comment == null) return Unauthorized();
                await _commentService.Commit();

                return Ok();

            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [Authorize]
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteEventComment([FromRoute]int commentId)
        {
            try
            {
                var userId = User.GetClaim(JwtTypes.jti);

                var user = await _userService.GetUserById(userId);
                var userRole = User.GetClaim(JwtTypes.Role);

                var comment = await _commentService.DeleteComment(commentId, user.Id, userRole);

                if (comment == null) return Unauthorized();

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