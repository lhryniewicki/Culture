using Culture.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
    public interface IEmailService
    {
        Task SendEmail(string content, IEnumerable<AppUser> appUsers);
    }
}
