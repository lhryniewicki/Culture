using Culture.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.ViewModels
{
    public class NotificationListViewModel
    {
        public IEnumerable<NotificationDto> Notifications{ get; set; }

        public NotificationListViewModel(IEnumerable<NotificationDto> notifications)
        {
            Notifications = notifications;
        }
    }
}
