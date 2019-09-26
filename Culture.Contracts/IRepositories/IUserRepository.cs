using Culture.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
	public interface IUserRepository
	{
        Task<IEnumerable<Guid>> GetEventParticipants(int id);
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByName(string name);
        Task<IList<string>> GetUserRoles(AppUser user);
        Task<AppUser> GetUserByNameWithCalendar(string userName);
        bool IsUserSigned(Guid userId, int eventId);
        Task<AppUser> GetUserByIdWithCalendar(Guid  userId);



    }
}