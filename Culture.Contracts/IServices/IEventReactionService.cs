using Culture.Contracts.DTOs;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
    public interface IEventReactionService
    {
        Task<EventReactionsDto> GetReactions(int eventId, Guid userId);
    }
}
