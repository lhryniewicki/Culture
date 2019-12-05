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
using System.Text;
using System.Threading.Tasks;

namespace Culture.Implementation.Facades
{

    public class CommentsFacade : ICommentsFacade
    {
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly IEventService _eventService;
        private readonly INotificationService _notificationService;
        private readonly IFileService _fileService;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CommentsFacade(
            IUserService userService,
            ICommentService commentService,
            IEventService eventService,
            INotificationService notificationService,
            IFileService fileService,
            IConfiguration configuration,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _commentService = commentService;
            _eventService = eventService;
            _notificationService = notificationService;
            _fileService = fileService;
            _configuration = configuration;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CommentDto> CreateComment(CommentViewModel commentViewModel)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var user = await _userService.GetUserById(userId);

            var eventModel = await _eventService.GetEventAsync(commentViewModel.EventId);

            var notificationTargets = new List<Guid>() { eventModel.CreatedById };

            string imagePath = null;

            if (commentViewModel.Image != null) imagePath = await _fileService.UploadImage(commentViewModel.Image);

            var commentDto = await _commentService.CreateCommentAsync(commentViewModel.Content, commentViewModel.EventId, user.Id, user.UserName, imagePath);
            var notification = await _notificationService.CreateNotificationsAsync($"Twoje wydarzenie zostało skomentowane: {eventModel.Name}", notificationTargets, eventModel.Id, eventModel.UrlSlug);

            var emailTarget = await _userService.GetUserById(eventModel.CreatedById.ToString());
            var emailContent = $"Twoje wydarzenie zostało skomentowane:  <a href='{_configuration["Values: MessageDomain"]}/wydarzenie/szczegoly/{eventModel.UrlSlug}'> Sprawdz szczegóły! </a>";

            await _emailService.SendEmail(emailContent, new List<AppUser>() { emailTarget });

            await _commentService.Commit();

            commentDto.Id =  _commentService.GetCommentId(commentDto);

            return commentDto;
        }

        public async Task DeleteEventComment(int commentId)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var user = await _userService.GetUserById(userId);
            var userRole = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.Role);

            var comment = await _commentService.DeleteComment(commentId, user.Id, userRole);

            await _commentService.Commit();
        }

        public async Task EditEventComment(EditCommentViewModel comment)
        {

            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);
            var userRole = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.Role);

            var user = await _userService.GetUserById(userId);

            var _comment = await _commentService.EditCommentAsync(comment, Guid.Parse(userId), userRole);

            await _commentService.Commit();

        }

        public async Task<CommentsListViewModel> GetEventComments(int eventId, int page = 0, int take = 5)
        {
            Guid userId;
            var res = Guid.TryParse(_httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti), out userId);
            userId = res == false ? Guid.Empty : userId;

            var userConfig = await _userService.GetUserConfiguration(userId);
            take = userConfig?.EventsDisplayAmount ?? 5;

            var commentBatchDto = await _commentService.GetEventCommentsAsync(eventId, page, take);

            return new CommentsListViewModel(commentBatchDto);
        }
    }
}
