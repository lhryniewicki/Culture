using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
    public interface IUserInEventRepository
    {
        Task SignUserToEvent(UserInEvent userInEvent);
        Task<UserInEvent> IsUserSigned(Guid userId, int eventId);
        Task UnsignUserFromEvent(int eventId, Guid userId);
    }
}
