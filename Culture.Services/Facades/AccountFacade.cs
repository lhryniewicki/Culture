using Culture.Contracts.DTOs;
using Culture.Contracts.Exceptions;
using Culture.Contracts.Facades;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using Culture.Utilities.Enums;
using Culture.Utilities.ExtensionMethods;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Implementation.Facades
{
    public class AccountFacade : IAccountFacade
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountFacade(
            IAuthService authService,
            IUserService userService,
            IFileService fileService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authService = authService;
            _userService = userService;
            _fileService = fileService;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<bool> CheckAnswer(string username, string answer)
        {
            var isAnswerCorrect = await _authService.CheckAnswer(username,answer);

            return isAnswerCorrect;
        }

        public async Task<UserDetailsDto> GetUserData(string userId)
        {
            var user = await _userService.GetUserById(userId);

            var userData = await _userService.GetUserDetailsByName(user.UserName);

            return userData;
        }

        public async Task<string> GetUserSecretQuestion(string username)
        {
            var question = await _authService.GetUserQuestion(username);

            return question;
        }

        public async Task<string> Login(LoginViewModel loginViewModel)
        {
                var token = await _authService.Login(loginViewModel);

                return token;
        }
        public async Task<string> UnloggedUser()
        {
            var token = await _authService.UnloggedUser();

            return token;
        }


        public async Task<string> Register(RegisterViewModel registerViewModel)
        {
            var token = await _authService.Register(registerViewModel);

            return token;
        }

        public async Task SendPassword(string username, string password)
        {
            await _authService.UpdatePassword(username, password);

            await _userService.Commit();
        }

        public async Task UpdateUserConfig(UpdateUserConfigViewModel userConfig)
        {
            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var updated = await _userService.UpdateUserConfig(userConfig, Guid.Parse(userId));

            await _userService.Commit();
        }

        public async Task UpdateUserData(UpdateUserViewModel userData)
        {
            var emailUnique = await _userService.GetUserByEmail(userData.Email);
            if (emailUnique != null && userData.Username != emailUnique.UserName)
            {
                throw new RegistrationErrorException("Email jest w użyciu.");

            }

            var userId = _httpContextAccessor.HttpContext.User.GetClaim(JwtTypes.jti);

            var avatarPath = await _fileService.UploadImage(userData.Image);

            var oldAvatarPath = await _userService.UpdateUserData(userId, userData, avatarPath);

            if (oldAvatarPath != avatarPath && avatarPath != null) _fileService.RemoveImage(oldAvatarPath);

            await _userService.Commit();
        }
    }
}
