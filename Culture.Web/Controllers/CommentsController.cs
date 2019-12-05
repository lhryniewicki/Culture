using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Culture.Contracts.Facades;
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
        private readonly ICommentsFacade _commentsFacade;

        public CommentsController(ICommentsFacade commentsFacade )
        {
            _commentsFacade = commentsFacade;
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPost("create")]
        public async Task<JsonResult> CreateComment([FromForm]CommentViewModel commentViewModel)
        {
            try
            {
                var commentDto = await _commentsFacade.CreateComment(commentViewModel);

                return Json(commentDto);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [Authorize(Roles = "User,Admin")]
        [AllowAnonymous]
        [HttpGet("get")]
        public async Task<JsonResult> GetEventComments(int eventId, int page=0, int take=5)
        {
            try
            {
                var commentVM = await _commentsFacade.GetEventComments(eventId, page, take);

                return Json(commentVM);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPut("edit")]
        public async Task<IActionResult> EditEventComment([FromBody]EditCommentViewModel comment)
        {
            try
            {
                await _commentsFacade.EditEventComment(comment);

                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
        }

        [Authorize(Roles = "User,Admin")]
        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteEventComment([FromRoute]int commentId)
        {
            try
            {
                await _commentsFacade.DeleteEventComment(commentId);

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