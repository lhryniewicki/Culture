using Culture.Contracts.IRepositories;
using Culture.DataAccess.Context;
using Culture.Models;
using System;
using System.Collections.Generic;
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
		public void RemoveReaction(EventReaction eventReaction)
		{
			_dbContext.EventReactions.Remove(eventReaction);
		}
	}
}
