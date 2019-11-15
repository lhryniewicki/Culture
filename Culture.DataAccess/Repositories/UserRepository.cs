﻿using Culture.Contracts.IRepositories;
using Culture.DataAccess.Context;
using Culture.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.DataAccess.Repositories
{
	public class UserRepository : IUserRepository
	{

        private readonly UserManager<AppUser> _userManager;

        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<Guid>> GetEventParticipants(int id)
        {
            return await _userManager.Users
                .Where(x => x.ParticipatedEvents.Any(y => y.EventId == id))
                .Select(x => x.Id)
                .ToListAsync();
        }

        public async Task<bool> IsUserReactionOwner(Guid userId, int eventId)
        {
            return  await _userManager.Users
                .FirstOrDefaultAsync(x => x.EventReactions.Any(y => y.EventId == eventId && y.UserId == userId)) !=null ? true : false;
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<AppUser> GetUserByName(string name)
        {
            return await _userManager.FindByNameAsync(name);
        }

        public async Task<AppUser> GetUserByNameWithCalendar(string userName)
        {
            return await _userManager.Users
                 .Include(x => x.Calendar)
                 .FirstOrDefaultAsync(x => x.UserName == userName);
        }

        public async Task<IList<string>> GetUserRoles(AppUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<AppUser> GetUserByIdWithCalendar(Guid userId)
        {
            return await _userManager.Users
                .Include(x => x.Calendar)
                    .ThenInclude(x=>x.Events)
                        .ThenInclude(x=>x.Event)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == userId);
        }
    }
}
