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
    }
}
