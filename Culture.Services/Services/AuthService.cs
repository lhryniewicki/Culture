using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Culture.Contracts.Exceptions;
using Culture.Contracts.IRepositories;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Culture.Services.Services
{
	public class AuthService:IAuthService
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;

		public AuthService(
			SignInManager<AppUser> signInManager,
			UserManager<AppUser> userManager,
			IConfiguration configuration)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_configuration = configuration;
		}

		public async Task<string> Login(LoginViewModel loginViewModel)
		{
			 ValidateLoginViewModel(loginViewModel);

			var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

			if (!result.Succeeded)
			{
				throw new LoginErrorException($"Error while logging");
			}

			var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
			return GetToken(user);
		}

		public async Task<string> Register(RegisterViewModel registerViewModel)
		{
			await ValidateRegisterViewModel(registerViewModel);

			var appUser = new AppUser()
			{
				Email = registerViewModel.Email,
				FirstName = registerViewModel.FirstName,
				UserName= registerViewModel.UserName,
				LastName = registerViewModel.LastName,                
			};

			var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);

			if (!result.Succeeded) throw new RegistrationErrorException("Error creating account!");

			var loginResult = await _signInManager.PasswordSignInAsync(registerViewModel.UserName, registerViewModel.Password, false, false);

			if (!loginResult.Succeeded)
			{
				throw new RegistrationErrorException($"Error while logging");
			}

			return GetToken(appUser);
		}

		private void  ValidateLoginViewModel(LoginViewModel loginViewModel)
		{

			if (string.IsNullOrEmpty(loginViewModel.Password))
			{
				throw new LoginErrorException("Password cannot be null or empty!");
			}

			if (string.IsNullOrEmpty(loginViewModel.UserName))
			{
				throw new LoginErrorException("UserName cannot be null or empty!");
			}
		}

		private async Task ValidateRegisterViewModel(RegisterViewModel registerViewModel)
		{
			try
			{
				var email = new MailAddress(registerViewModel.Email);
			}
			catch
			{
				throw new RegistrationErrorException("Email address is invalid!");
			}

			var emailUnique = await _userManager.FindByEmailAsync(registerViewModel.Email);

			if (emailUnique != null)
			{
				throw new RegistrationErrorException("Email is not unique!");

			}
			if (string.IsNullOrEmpty(registerViewModel.Password))
			{
				throw new RegistrationErrorException("Password cannot be null or empty!");
			}

			if (string.IsNullOrEmpty(registerViewModel.UserName))
			{
				throw new RegistrationErrorException("UserName cannot be null or empty!");
			}
			if (string.IsNullOrEmpty(registerViewModel.FirstName))
			{
				 throw new RegistrationErrorException("FirstName cannot be null or empty!");
			}
			if (string.IsNullOrEmpty(registerViewModel.LastName))
			{
				throw new RegistrationErrorException("LastName cannot be null or empty!");
			}
		}

		private string GetToken(AppUser user)
		{
			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
			};
			var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Values:IssuerToken")));
			var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
			var utcNow = DateTime.UtcNow;

			var jst = new JwtSecurityToken(
				signingCredentials: signingCredentials,
				claims: claims,
				notBefore: utcNow,
				expires: utcNow.AddSeconds(_configuration.GetValue<int>("Values:TokenLifetime"))
			);

			return new JwtSecurityTokenHandler().WriteToken(jst);
		}
	}
}
