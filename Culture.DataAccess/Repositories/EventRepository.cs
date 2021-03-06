﻿using Culture.Contracts;
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
                .Include(x=>x.Participants)
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
                            && (query != null ? x.Content.IndexOf(query,StringComparison.OrdinalIgnoreCase) !=-1 
                            || x.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1 : true ))
                .OrderByDescending(x=>x.CreationDate)
                .Skip(page * size)
                .Take(size+1)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<IEnumerable<Event>> GetAllEvents(string query = null, IEnumerable<string[]> dates = null, string category = null)
        {
            var events = _dbContext.Events.AsQueryable();

            events = category != null ? events.Where(x => x.Category == category) : events;

            events = query != null ? events.Where(x => x.Content.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1 
            || x.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1 ) : events;

            if (dates != null && dates.ElementAt(0) != null)
            {

                var dateStart = convertDate(dates.ElementAt(0));
                events = events.Where(x => DateTime.Compare(x.TakesPlaceDate.Date,dateStart.Date) >= 0);

                if (dates.Count() > 1)
                {
                    events = events.Where(x => DateTime.Compare(x.TakesPlaceDate.Date ,convertDate(dates.ElementAt(1)).Date) <= 0);
                }

            }

            return events;
        }
        private DateTime convertDate(string[] date)
        {
            var intDate = Array.ConvertAll(date, Int32.Parse);

            return new DateTime(intDate[0], intDate[1], intDate[2]);
        }

        public async Task<IEnumerable<Event>> GetRecommendedEvents(Event queryEvent, int skip=0 , int take=3)
        {
            var nameWords = queryEvent.Name.Split();

            return await _dbContext.Events
                .Where(x => nameWords.Contains(x.Name) && x.Id != queryEvent.Id)
                .Union(
                 _dbContext.Events.Where(x => x.Category == queryEvent.Category && x.Id != queryEvent.Id) )
                .Union(
                  _dbContext.Events.Where(x => x.CityName == queryEvent.CityName && x.Id != queryEvent.Id))
                .Union(_dbContext.Events.Where(x => x.Price < queryEvent.Price + 10 && x.Price > queryEvent.Price - 10 && x.Id != queryEvent.Id))
                .Skip(skip*take)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<AppUser>> GetParticipants(int eventId, string query = null)
        {
            return await _dbContext.Users
                 .Where(x => x.ParticipatedEvents
                     .Any(y => y.EventId == eventId) && (query != null ? (
                     x.FirstName.Contains(query) ||
                     x.LastName.Contains(query) ||
                     x.UserName.Contains(query)) : true))
                 .ToListAsync();
        }

        public async Task<IEnumerable<DateTime>> GetAllCalendar(string query = null, IEnumerable<string[]> dates=null, string category=null)
        {
            var events = _dbContext.Events.AsQueryable();

            events = category != null ? events.Where(x => x.Category == category) : events;

            events = query != null ? events.Where(x => x.Content.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1
            || x.Name.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1) : events;

            if (dates != null && dates.ElementAt(0) != null)
            {

                var dateStart = convertDate(dates.ElementAt(0));
                events = events.Where(x => DateTime.Compare(x.TakesPlaceDate.Date, dateStart.Date) >= 0);

                if (dates.Count() > 1)
                {
                    events = events.Where(x => DateTime.Compare(x.TakesPlaceDate.Date, convertDate(dates.ElementAt(1)).Date) <= 0);
                }

            }

            var datess = await events.Select(x=>x.TakesPlaceDate).ToListAsync();
            return datess;
        }

    }
}
