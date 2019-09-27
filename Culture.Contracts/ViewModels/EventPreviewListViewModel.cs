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
        public bool CanLoadMore { get; set; }
        public EventPreviewListViewModel(EventsPreviewWithLoadDto eventList)
        {
            Events = eventList.Events;
            CanLoadMore = eventList.CanLoadMore;
        }
    }
}
