using Culture.Contracts.IServices;
using Culture.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

		public  Task<AppUser> GetUserById(string id)
		{
			return  _userManager.FindByIdAsync(id);
		}

		public  Task<AppUser> GetUserByName(string name)
		{
			return _userManager.FindByNameAsync(name);
		}

        public Task<AppUser> GetUserByNameWithCalendar(string userName)
        {
            return _userManager.Users
                 .Include(x => x.Calendar)
                 .FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public  Task<IList<string>> GetUserRoles(AppUser user)
        {
            return  _userManager.GetRolesAsync(user);
        }
    }
}
