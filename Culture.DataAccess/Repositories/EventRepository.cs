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

		public async Task AddEventAsync(Event eventt)
		{
			await _dbContext.Events.AddAsync(eventt);
			await _dbContext.SaveChangesAsync();
		}

		public Task<Event> GetEventDetailsAsync(int id)
		{
			return  _dbContext.Events
				.Include(x=>x.Participants)
				.Include(x=>x.Reactions)
				.Include(x=>x.Comments)
				.Include(x=>x.CreatedBy)
				.SingleOrDefaultAsync(x => x.Id == id);

		}
	}
}
