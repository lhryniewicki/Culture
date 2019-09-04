using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs.NotificationWebJob
{
	public class WebJobItem
	{
		public int EventId { get; set; }
		public IEnumerable<Guid> TargetUsers { get; set; }
	}
}
