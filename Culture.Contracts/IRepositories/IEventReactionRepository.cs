using Culture.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
	public interface IEventReactionRepository
	{
		void RemoveReaction(EventReaction eventReaction);
        Task<EventReaction> GetUserReactionAsync(Guid userId, int eventId);
        Task<IEnumerable<EventReaction>> GetEventReactions(int eventId);
    }
}
