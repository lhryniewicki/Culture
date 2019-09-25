using System;
using System.Collections.Generic;
using System.Text;
using Culture.Models;

namespace Culture.Contracts.DTOs
{
    public class EventReactionsDto
    {
        public IEnumerable<EventReactionDto> EventReactions { get; set; }
        public int TotalCount { get; set; }
        public string CurrentReaction { get; set; }
    }
}
