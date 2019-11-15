namespace Culture.Models
{
    public class EventInCalendar
	{
		public int EventId { get; set; }
		public Event Event { get; set; }

		public int CalendarId { get; set; }
		public Calendar Calendar { get; set; }


	}
}
