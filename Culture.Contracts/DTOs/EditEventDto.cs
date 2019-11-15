using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class EditEventDto
    {
        public bool NeedsGeolocation { get; set; }
        public Event EditedEvent{ get; set; }
    }
}
