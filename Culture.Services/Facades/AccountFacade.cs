using Culture.Contracts.Facades;
using Culture.Contracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Implementation.Facades
{
    public class AccountFacade : IAccountFacade
    {
        public async Task<bool> CheckAnswer(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public Task<JsonResult> GetUserData(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<JsonResult> GetUserSecretQuestion(string username)
        {
            throw new NotImplementedException();
        }

        public Task<JsonResult> Login(LoginViewModel loginViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<JsonResult> Register(RegisterViewModel registerViewModel)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> SendPassword(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateUserConfig(UpdateUserConfigViewModel userConfig)
        {
            throw new NotImplementedException();
        }

        public Task<IActionResult> UpdateUserData(UpdateUserViewModel userData)
        {
            throw new NotImplementedException();
        }
    }
}
