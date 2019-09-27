using Culture.Contracts;
using Culture.Contracts.IRepositories;
using Culture.DataAccess.Context;
using Culture.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.DataAccess.Repositories
{
    public class UserInEventRepository : IUserInEventRepository
    {
        private readonly CultureDbContext _dbContext;

        public UserInEventRepository(CultureDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task SignUserToEvent(UserInEvent userInEvent)
        {
            return _dbContext.UsersInEvent.AddAsync(userInEvent);
        }
        public  Task<UserInEvent> IsUserSigned(Guid userId, int eventId)
        {
            return  _dbContext.UsersInEvent
                .FirstOrDefaultAsync(x => x.EventId == eventId && userId==x.UserId);
        }

        public async Task UnsignUserFromEvent(int eventId,Guid userId)
        {
            var toDelete = await _dbContext.UsersInEvent.FirstOrDefaultAsync(x => x.EventId == eventId && x.UserId == userId);
            _dbContext.UsersInEvent.Remove(toDelete);
        }
    }
}
