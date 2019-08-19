using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Culture.Models
{
	public class AppUser:IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public ICollection<UserInEvent> ParticipatedEvents{ get; set; }
		public ICollection<Event> HostedEvents { get; set; }
		public ICollection<EventReaction> EventReactions { get; set; }
		public ICollection<Notification> Notifications { get; set; }
		public ICollection<Comment> Comments { get; set; }

		public int CalendarId { get; set; }
		public Calendar Calendar { get; set; }

		public AppUser()
		{
			ParticipatedEvents = new List<UserInEvent>();
			HostedEvents = new List<Event>();
			EventReactions = new List<EventReaction>();
			Notifications = new List<Notification>();
            Comments = new List<Comment>();
		}
	}
}
