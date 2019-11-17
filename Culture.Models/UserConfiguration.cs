using System;

namespace Culture.Models
{
    public class UserConfiguration
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public int CommentsDisplayAmount { get; set; } = 5;
        public int EventsDisplayAmount { get; set; } = 5;
        public int LogOutAfter { get; set; } = 10;
        public bool Anonymous { get; set; } = true;
        public bool SendEmailNotification { get; set; } = false;
        public bool CalendarPastEvents { get; set; } = true;
    }
}
