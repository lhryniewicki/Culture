using Culture.Contracts.DTOs;
using Culture.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.Facades
{
    public interface IAccountFacade
    {
        Task<string> Login(LoginViewModel loginViewModel);
        Task<string> UnloggedUser();
        Task<string> Register( RegisterViewModel registerViewModel);
        Task<UserDetailsDto> GetUserData(string userId);
        Task UpdateUserData( UpdateUserViewModel userData);
        Task UpdateUserConfig( UpdateUserConfigViewModel userConfig);
        Task<string> GetUserSecretQuestion(string username);
        Task<bool> CheckAnswer(string username, string answer);
        Task SendPassword(string username, string password);
    }
}
