using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Models
{
	public class UserInEvent
	{
		public Guid UserId { get; set; }
		public AppUser User { get; set; }

		public int EventId { get; set; }
		public Event Event { get; set; }

	}
}
