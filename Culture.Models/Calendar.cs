using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Models
{
	public class Calendar
	{
		public int Id { get; set; }
		public ICollection<EventInCalendar> Events { get; set; }

		public Guid BelongsToId { get; set; }
		public AppUser BelongsTo { get; set; }

		public Calendar()
		{
			Events = new List<EventInCalendar>();
		}

	}
}
