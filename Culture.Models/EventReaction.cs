using Culture.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Models
{
	public class EventReaction
	{
		public ReactionType Type{ get; set; }
		public int EventId { get; set; }
		public Event Event { get; set; }
		public Guid UserId { get; set; }
		public AppUser User { get; set; }
	}
}
