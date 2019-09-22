using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs
{
   public class EventReactionsWAuthorDto
    {
        public Guid Id { get; set; }
        public IEnumerable<EventReactionDto> Reactions { get; set; }
    }
}
