using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Culture.Contracts.Exceptions;
using Culture.Contracts.Facades;
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
        private readonly IAccountFacade _accountFacade;

        public AccountController(IAccountFacade accountFacade)
		{
            _accountFacade = accountFacade;
        }

		[HttpPost("login")]
		public async Task<JsonResult> Login([FromBody] LoginViewModel loginViewModel)
		{
			try
			{
				var token = await _accountFacade.Login(loginViewModel);
                
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

        [HttpPost("unlogged")]
        public async Task<JsonResult> UnloggedUser()
        {
            try
            {
                var token = await _accountFacade.UnloggedUser();

                return Json(token);
            }
            catch (LoginErrorException e)
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
				var token = await _accountFacade.Register(registerViewModel);

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
            try
            {
                var userData = await _accountFacade.GetUserData(userId);

                return Json(userData);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }

        }

        [Authorize(Roles = "User,Admin")]
        [HttpPut("user")]
        public async Task<IActionResult> UpdateUserData([FromForm] UpdateUserViewModel userData)
        {
            try
            {
                await _accountFacade.UpdateUserData(userData);

                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
           
        }

        [Authorize(Roles = "User,Admin")]
        [HttpPut("user/config")]
        public async Task<IActionResult> UpdateUserConfig([FromBody] UpdateUserConfigViewModel userConfig)
        {
            try
            {
                await _accountFacade.UpdateUserConfig(userConfig);

                return Ok();
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
           
        }

        [HttpGet("user/question")]
        public async Task<JsonResult> GetUserSecretQuestion(string username)
        {
            try
            {
                var question = await _accountFacade.GetUserSecretQuestion(username);

                return Json(question);
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return Json(e.Message + e.InnerException);
            }
            
        }

        [HttpGet("user/answer")]
        public async Task<bool> CheckAnswer(string username, string answer)
        {
            try
            {
                var isAnswerCorrect = await _accountFacade.CheckAnswer(answer, username);

                return isAnswerCorrect;
            }
            catch (Exception e)
            {
                Response.StatusCode = 500;
                return false;
            }

        }

        [HttpGet("user/password")]
        public async Task<IActionResult> SendPassword(string username, string password)
        {
            try
            {
                await _accountFacade.SendPassword(username, password);

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
