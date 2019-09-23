using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Models
{
	public class Event
	{
		public int Id { get; set; }
		public int Price { get; set; }
		public string Name { get; set; }
		public string Content { get; set; }
		public string ImagePath { get; set; }
		public string Category { get; set; } //zrobic enum
		public string StreetName { get; set; }
		public string CityName { get; set; }
        public string UrlSlug { get; set; }
        public DateTime CreationDate { get; set; }
		public DateTime TakesPlaceDate { get; set; }
		public ICollection<UserInEvent> Participants { get; set; }
		public ICollection<Comment> Comments { get; set; }
		public ICollection<EventReaction> Reactions { get; set; }
		public ICollection<EventInCalendar> EventsInCalendar { get; set; }
		public ICollection<Notification> Notifications { get; set; }

		public Guid CreatedById { get; set; }
		public AppUser CreatedBy { get; set; }

		public Event()
		{
			Participants = new List<UserInEvent>();
			Comments = new List<Comment>();
			Reactions = new List<EventReaction>();
			EventsInCalendar = new List<EventInCalendar>();
			Notifications = new List<Notification>();
		}


	}
}
