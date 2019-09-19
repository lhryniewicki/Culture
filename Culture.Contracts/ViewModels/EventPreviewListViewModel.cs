using Culture.Contracts.DTOs;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.ViewModels
{
    public class EventPreviewListViewModel
    {
        public IEnumerable<EventsPreviewDto> Events { get; set; }

    }
}
