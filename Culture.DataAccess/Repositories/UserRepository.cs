using Culture.Contracts.IRepositories;
using Culture.DataAccess.Context;
using Culture.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Culture.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
	{

        private readonly UserManager<AppUser> _userManager;
        private readonly CultureDbContext _cultureDbContext;

        public UserRepository(
            UserManager<AppUser> userManager,
            CultureDbContext cultureDbContext
            )
        {
            _userManager = userManager;
            _cultureDbContext = cultureDbContext;
        }

        public async Task<IEnumerable<AppUser>> GetEventParticipants(int id)
        {
            return await _userManager.Users
                .Where(x => x.ParticipatedEvents.Any(y => y.EventId == id))
                .ToListAsync();
        }

        public async Task<bool> IsUserReactionOwner(Guid userId, int eventId)
        {
            return  await _userManager.Users
                .FirstOrDefaultAsync(x => x.EventReactions.Any(y => y.EventId == eventId && y.UserId == userId)) !=null ? true : false;
        }

        public Task<AppUser> GetUserById(string id)
        {
            return _userManager.FindByIdAsync(id);
        }

        public Task<AppUser> GetUserByName(string name)
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

        public Task<AppUser> GetUserByIdWithCalendar(Guid userId)
        {
            return  _userManager.Users
                .Include(x => x.Calendar)
                    .ThenInclude(x=>x.Events)
                        .ThenInclude(x=>x.Event)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);
        }

        public Task<UserConfiguration> GetUserConfiguration(Guid userId)
        {
            return  _cultureDbContext
                .UserConfigurations
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public Task<AppUser> GetUserByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email);
        }
        public Task<IdentityResult> SetEmail(AppUser user,string email)
        {
            return _userManager.SetEmailAsync(user, email);
        }

    }
}
