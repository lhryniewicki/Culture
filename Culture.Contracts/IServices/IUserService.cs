using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
	public interface IUserService
	{
		Task<AppUser> GetUserById(string id);
		Task<AppUser> GetUserByName(string name);
        Task<IList<string>> GetUserRoles(AppUser user);
        Task<AppUser> GetUserByNameWithCalendar(string userName);
        Task<AppUser> GetUserByEventId(Guid id);


    }
}
