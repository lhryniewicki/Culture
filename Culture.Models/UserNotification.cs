using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Models
{
	public class UserNotification
	{
		public Guid UserId { get; set; }
		public AppUser User { get; set; }

		public int NotificationId { get; set; }
		public Notification Notification { get; set; }

	}
}
