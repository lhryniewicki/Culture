using Culture.Contracts.DTOs.NotificationWebJob;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.Dtos.NotificationWebJob
{
    public class NotificationWebJob
    {
		public IEnumerable<WebJobItem> Notifications { get; set; }
    }
}
