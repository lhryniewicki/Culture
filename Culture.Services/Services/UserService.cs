using Culture.Contracts.IServices;
using Culture.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
	public class UserService:IUserService
	{
		private readonly UserManager<AppUser> _userManager;

		public UserService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<AppUser> GetUserById(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
			return user;
		}

		public async Task<AppUser> GetUserByName(string name)
		{
			var user = await _userManager.FindByNameAsync(name);
			return user;
		}
	}
}
