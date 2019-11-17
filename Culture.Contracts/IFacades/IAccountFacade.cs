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
        Task<JsonResult> Login(LoginViewModel loginViewModel);
        Task<JsonResult> Register( RegisterViewModel registerViewModel);
        Task<JsonResult> GetUserData(string userId);
        Task<IActionResult> UpdateUserData( UpdateUserViewModel userData);
        Task<IActionResult> UpdateUserConfig( UpdateUserConfigViewModel userConfig);
        Task<JsonResult> GetUserSecretQuestion(string username);
        Task<bool> CheckAnswer(string username, string answer);
        Task<IActionResult> SendPassword(string username, string password);
    }
}
