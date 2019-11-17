using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Culture.Models
{
    public class AppUser:IdentityUser<Guid>
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
        public string AvatarPath { get; set; }
        public string SecretQuestion { get; set; }
        public string SecretAnswer { get; set; }
        public ICollection<UserInEvent> ParticipatedEvents{ get; set; }
		public ICollection<Event> HostedEvents { get; set; }
		public ICollection<EventReaction> EventReactions { get; set; }
		public ICollection<Notification> Notifications { get; set; }
		public ICollection<Comment> Comments { get; set; }

        public Guid UserConfigurationId { get; set; }
        public UserConfiguration UserConfiguration { get; set; }

        public int CalendarId { get; set; }
		public Calendar Calendar { get; set; }

		public AppUser()
		{
			ParticipatedEvents = new List<UserInEvent>();
			HostedEvents = new List<Event>();
			EventReactions = new List<EventReaction>();
			Notifications = new List<Notification>();
            Comments = new List<Comment>();
            Calendar = new Calendar();
            UserConfiguration = new UserConfiguration();
		}
	}
}
