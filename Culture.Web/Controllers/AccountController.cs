using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Culture.Contracts.Exceptions;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Utilities.Enums;
using Culture.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Culture.Web.Controllers
{
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public AccountController(
            IAuthService authService,
            IUserService userService,
            IFileService fileService)
		{
			_authService = authService;
            _userService = userService;
            _fileService = fileService;
        }

		[HttpPost("login")]
		public async Task<JsonResult> Login([FromBody] LoginViewModel loginViewModel)
		{
			try
			{
				var token = await _authService.Login(loginViewModel);
                
				return Json(token);
			}
			catch(LoginErrorException e)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json(e.Message);
			}
			catch (Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message);
			}
		}

		[HttpPost("register")]
		public async Task<JsonResult> Register([FromBody] RegisterViewModel registerViewModel)
		{

			try
			{
				var token = await _authService.Register(registerViewModel);

                return Json(token);
			}
			catch (RegistrationErrorException e)
			{
				Response.StatusCode = (int)HttpStatusCode.BadRequest;
				return Json(e.Message);
			}
			catch(Exception e)
			{
				Response.StatusCode = 500;
				return Json(e.Message);
			}
		}
        [HttpGet("user")]
        public async Task<JsonResult> GetUserData(string userId)
        {   
            var user = await _userService.GetUserById(userId);

            var userData = await _userService.GetUserDetailsByName(user.UserName);

            return Json(userData);
        }

        [Authorize]
        [HttpPut("user")]
        public async Task<IActionResult> UpdateUserData([FromForm] UpdateUserViewModel userData)
        {
            var userId = User.GetClaim(JwtTypes.jti);

            var avatarPath = await _fileService.UploadImage(userData.Image);

            var oldAvatarPath = await _userService.UpdateUserData(userId, userData, avatarPath);

            if (oldAvatarPath != avatarPath && avatarPath != null) _fileService.RemoveImage(oldAvatarPath);

            await _userService.Commit();

            return Ok();
        }

        [Authorize]
        [HttpPut("user/config")]
        public async Task<IActionResult> UpdateUserConfig([FromBody] UpdateUserConfigViewModel userConfig)
        {
            var userId = User.GetClaim(JwtTypes.jti);

            var updated = await _userService.UpdateUserConfig(userConfig, Guid.Parse(userId));

            if (updated == null) return Unauthorized();

            await _userService.Commit();

            return Ok();
        }

        [HttpGet("user/question")]
        public async Task<JsonResult> GetUserSecretQuestion(string username)
        {

            var question = await _authService.GetUserQuestion(username);

            return Json(question);
        }

        [HttpGet("user/answer")]
        public async Task<bool> CheckAnswer(string username, string answer)
        {
            var isAnswerCorrect = await _authService.CheckAnswer(answer, username);

            return isAnswerCorrect;
        }

        [HttpGet("user/password")]
        public async Task<IActionResult> SendPassword(string username, string password)
        {

            await _authService.UpdatePassword(username,password);

            await _userService.Commit();
            return Ok();
        }
    }
}
