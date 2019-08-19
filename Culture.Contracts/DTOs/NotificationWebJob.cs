using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class NotificationWebJob
    {
        public string Content { get; set; }
        public int EventId { get; set; }
        public IEnumerable<Guid> TargetUsers { get; set; }

    }
}
