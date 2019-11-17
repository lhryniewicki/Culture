using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.ViewModels
{
    public class UpdateUserConfigViewModel
    {
        public int CommentsDisplayAmount { get; set; } 
        public int EventsDisplayAmount { get; set; } 
        public int LogOutAfter { get; set; } 
        public bool Anonymous { get; set; }
        public bool SendEmailNotification { get; set; } 
        public bool CalendarPastEvents { get; set; } 
    }
}
