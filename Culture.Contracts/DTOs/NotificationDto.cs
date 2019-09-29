using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class NotificationDto
    {
        public string Content { get; set; }
        public string Date { get; set; }
        public string RedirectUrl { get; set; }

    }
}
