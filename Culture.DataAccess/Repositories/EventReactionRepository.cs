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
	public class EventReactionRepository : IEventReactionRepository
	{
		private readonly CultureDbContext _dbContext;

		public EventReactionRepository(CultureDbContext dbContext)
		{
			_dbContext = dbContext;
		}

        public Task<EventReaction> GetUserReactionAsync(Guid userId, int eventId)
        {
            return _dbContext.EventReactions
                .FirstOrDefaultAsync(x => x.EventId == eventId && x.UserId == userId);
        }
        public async Task<IEnumerable<EventReaction>> GetEventReactions(int eventId)
        {
            return await _dbContext.EventReactions
                .Where(x=>x.EventId==eventId)
                .ToListAsync();
            
        }
        public void RemoveReaction(EventReaction eventReaction)
		{
			_dbContext.EventReactions.Remove(eventReaction);
		}

	}
}
