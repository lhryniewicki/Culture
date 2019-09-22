using Culture.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.ViewModels
{
    public class SortedReactionsViewModel
    {
        public IEnumerable<EventReactionDto> Reactions { get; set; }

        public SortedReactionsViewModel()
        {
            Reactions = new List<EventReactionDto>();
        }

        public SortedReactionsViewModel(IEnumerable<EventReactionDto> reactions)
        {
            Reactions = reactions;
        }
    }
}
