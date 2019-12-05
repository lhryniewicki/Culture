using Culture.Contracts;
using Culture.Contracts.Exceptions;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
    public class AuthService:IAuthService
	{
		private readonly SignInManager<AppUser> _signInManager;
		private readonly UserManager<AppUser> _userManager;
		private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(
			SignInManager<AppUser> signInManager,
			UserManager<AppUser> userManager,
			IConfiguration configuration,
            IUnitOfWork unitOfWork)
		{
			_signInManager = signInManager;
			_userManager = userManager;
			_configuration = configuration;
            _unitOfWork = unitOfWork;
        }

		public async Task<string> Login(LoginViewModel loginViewModel)
		{
			 ValidateLoginViewModel(loginViewModel);

			var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);

			if (!result.Succeeded)
			{
				throw new LoginErrorException($"Nazwa użytkownika albo hasło są nieprawidłowe.");
			}

			var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
			return await GetToken(user);
		}
        public async Task<string> UnloggedUser()
        {
            return await GetUnloggedToken();
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
                SecretQuestion = registerViewModel.SecretQuestion,
                SecretAnswer = registerViewModel.SecretAnswer

			};

			var result = await _userManager.CreateAsync(appUser, registerViewModel.Password);

			if (!result.Succeeded) throw new RegistrationErrorException("Bład podczas rejestracji. Skontaktuj się z administratorem systemu.");

			var loginResult = await _signInManager.PasswordSignInAsync(registerViewModel.UserName, registerViewModel.Password, false, false);

			if (!loginResult.Succeeded)
			{
				throw new RegistrationErrorException($"Błąd podczas logowania po rejestracji. Skontaktuj się z administratorem systemu.");
			}

			return await GetToken(appUser);
		}


        public async Task<string> GetUserQuestion(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return user.SecretQuestion;
        }

        public async Task<bool> CheckAnswer(string answer, string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            return user.SecretAnswer == answer;
        }

        public async Task UpdatePassword(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            await _userManager.RemovePasswordAsync(user);

            await _userManager.AddPasswordAsync(user, password);
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
				throw new RegistrationErrorException("Błędny adres Email.");
			}

            var userNameUnique = await _userManager.FindByNameAsync(registerViewModel.UserName);

            var emailUnique = await _userManager.FindByEmailAsync(registerViewModel.Email);
            if (emailUnique != null)
            {
                throw new RegistrationErrorException("Email jest w użyciu.");

            }

            if (userNameUnique != null)
            {
                throw new RegistrationErrorException("Nazwa użytkownika nie jest unikatowa");

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

		private async Task<string> GetToken(AppUser user)
		{
            var config = await _unitOfWork.UserRepository.GetUserConfiguration(user.Id);
            var expirationTime = config?.LogOutAfter ?? 5;

            var userRoles = await _userManager.GetRolesAsync(user);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim("Role",userRoles.Contains("Admin") ? "Admin" : "User")
			};

			var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Values:IssuerToken")));
			var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
			var utcNow = DateTime.UtcNow;

			var jst = new JwtSecurityToken(
				signingCredentials: signingCredentials,
				claims: claims,
				notBefore: utcNow,
				expires: utcNow.AddSeconds(expirationTime*60)
			);

			return new JwtSecurityTokenHandler().WriteToken(jst);
		}

        private async Task<string> GetUnloggedToken()
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Values:IssuerToken")));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var utcNow = DateTime.UtcNow;

            var jst = new JwtSecurityToken(
                signingCredentials: signingCredentials,
                claims: claims,
                notBefore: utcNow,
                expires: utcNow.AddSeconds(6000)
            );

            return new JwtSecurityTokenHandler().WriteToken(jst);
        }
    }
}
