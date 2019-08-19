using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Culture.Contracts.Exceptions;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Culture.Web.Controllers
{
	[Route("api/[controller]")]
	public class AccountController : Controller
	{
		private readonly IAuthService _authService;

		public AccountController(IAuthService authService)
		{
			_authService = authService;
		}
		// GET: /<controller>/
		public IActionResult Index()
		{
			return View();
		}
		[HttpGet("login")]

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
	}
}
