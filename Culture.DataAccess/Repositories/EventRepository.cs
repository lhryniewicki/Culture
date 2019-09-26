using Culture.Contracts;
using Culture.Contracts.IRepositories;
using Culture.DataAccess.Context;
using Culture.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.DataAccess.Repositories
{
	public class EventRepository : IEventRepository
	{
		private readonly CultureDbContext _dbContext;
		public EventRepository(CultureDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task CreateEventAsync(Event eventt)
		{
			await _dbContext.Events.AddAsync(eventt);
		}

        public void DeleteEvent(Event _event)
        {
            _dbContext.Events.Remove(_event);
        }

        public Task<Event> GetEventAsync(int id)
        {
            return _dbContext.Events
                .SingleOrDefaultAsync(x => x.Id == id);
        }
		public Task<Event> GetEventWithReactions(int id)
		{
			return _dbContext.Events
				.Include(x=>x.Reactions)
				.SingleOrDefaultAsync(x => x.Id == id);
		}

        public Task<Event> GetEventDetailsBySlugAsync(string slug)
		{
            return _dbContext.Events
                .Include(x => x.CreatedBy)
                .Include(x=>x.EventsInCalendar)
                    .ThenInclude(y=>y.Calendar)
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.UrlSlug == slug);
		}

        public async Task<IEnumerable<Event>> GetEventPreviewList(int page=0,int size=5, string category=null, string query = null)
        {
            return await _dbContext.Events
                .Include(x=>x.CreatedBy)
                .Where(x => (category != null ? x.Category == category : true ) 
                            && (query != null ? x.Content.Contains(query)|| x.Name.Contains(query):true ))
                .OrderByDescending(x=>x.CreationDate)
                .Skip(page * size)
                .Take(size)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
