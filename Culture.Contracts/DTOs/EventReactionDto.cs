using Culture.Models;
using Culture.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class EventReactionDto
    {

        public string ReactionType{ get; set; }
        public int Count { get; set; }
    }
}
