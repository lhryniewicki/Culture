using Culture.Contracts;
using Culture.Contracts.IRepositories;
using Culture.DataAccess.Context;
using Culture.Models;
using System;
using System.Collections.Generic;
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
    }
}
