using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Models
{
	public class Notification
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public bool IsRead{ get; set; }
		public ICollection<UserNotification> Users { get; set; }
		public DateTime SentData { get; set; }

		public int EventId { get; set; }
		public Event Event { get; set; }

		public Notification()
		{
			Users = new List<UserNotification>();
		}
	}
}
