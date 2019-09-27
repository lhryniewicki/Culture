using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class EventsPreviewWithLoadDto
    {
        public ICollection<EventsPreviewDto> Events { get; set; }
        public bool CanLoadMore { get; set; }

        public EventsPreviewWithLoadDto()
        {
            Events = new List<EventsPreviewDto>();
        }
    }
}
