using Culture.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
	public interface IAuthService
	{
		Task<string> Login(LoginViewModel loginViewModel);
        Task<string> UnloggedUser();
        Task<string> Register(RegisterViewModel registerViewModel);
        Task<string> GetUserQuestion(string username);
        Task<bool> CheckAnswer(string answer, string userId);
        Task UpdatePassword(string username, string password);

    }
}
