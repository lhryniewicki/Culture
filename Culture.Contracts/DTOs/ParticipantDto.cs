using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class ParticipantDto
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
    }
}
