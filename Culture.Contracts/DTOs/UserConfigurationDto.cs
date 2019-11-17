using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class UserConfigurationDto
    {
        public int CommentsDisplayAmount { get; set; } = 5;
        public int EventsDisplayAmount { get; set; } = 5;
        public int LogOutAfter { get; set; } = 10;
        public bool Anonymous { get; set; } = true;
        public bool SendEmailNotification { get; set; } = false;
        public bool CalendarPastEvents { get; set; } = true;

        public UserConfigurationDto(UserConfiguration userConfiguration)
        {
            CommentsDisplayAmount = userConfiguration?.CommentsDisplayAmount ?? 5;
            EventsDisplayAmount = userConfiguration?.EventsDisplayAmount ?? 5;
            LogOutAfter = userConfiguration?.LogOutAfter ?? 10;
            Anonymous = userConfiguration?.Anonymous ?? true;
            SendEmailNotification = userConfiguration?.SendEmailNotification ?? false;
            CalendarPastEvents = userConfiguration?.CalendarPastEvents ?? true;
        }

        public UserConfigurationDto()
        {
        }
    }
}
