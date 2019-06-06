using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Models
{
	public class EventReaction
	{
		public string Name { get; set; }
		//jakis obrazek te zzdecydowac jak przechowac
		public int EventId { get; set; }
		public Event Event { get; set; }

		public Guid UserId { get; set; }
		public AppUser User { get; set; }
	}
}
