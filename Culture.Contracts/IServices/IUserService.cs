using Culture.Contracts.DTOs;
using Culture.Contracts.ViewModels;
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
        Task<AppUser> GetUserByEmail(string email);
        Task<AppUser> GetUserByName(string name);
        Task<IList<string>> GetUserRoles(AppUser user);
        Task<AppUser> GetUserByNameWithCalendar(string userName);
		Task<IEnumerable<AppUser>> GetEventParticipants(int id);
        Task<bool> IsUserSigned(Guid userId, int eventId);
        Task<IEnumerable<DateTime>> GetUserCalendarDays(Guid userId, string category, string query);
        Task<IEnumerable<EventInCalendarDto>> GetUserEventsInDay(Guid userId, DateTime day);
        Task<UserDetailsDto> GetUserDetailsByName(string  name);
        Task<UserConfiguration> GetUserConfiguration(Guid userId);
        Task<string> UpdateUserData(string userId, UpdateUserViewModel userData, string avatarPath);
        Task Commit();
        Task<UserConfiguration> UpdateUserConfig(UpdateUserConfigViewModel userConfig, Guid userId);
    }
}
