using Culture.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
	public interface IUserRepository
	{
        Task<IEnumerable<AppUser>> GetEventParticipants(int id);
        Task<AppUser> GetUserById(string id);
        Task<AppUser> GetUserByEmail(string email);
        Task<AppUser> GetUserByName(string name);
        Task<IList<string>> GetUserRoles(AppUser user);
        Task<AppUser> GetUserByNameWithCalendar(string userName);
        Task<AppUser> GetUserByIdWithCalendar(Guid  userId);
        Task<IdentityResult> SetEmail(AppUser user, string email);
        Task<bool> IsUserReactionOwner(Guid userId, int eventId);
        Task<UserConfiguration> GetUserConfiguration(Guid userId);
    }
}