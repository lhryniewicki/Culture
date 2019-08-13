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
		Task<string> Register(RegisterViewModel registerViewModel);

	}
}
