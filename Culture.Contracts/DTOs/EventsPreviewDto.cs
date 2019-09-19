using Culture.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.DTOs
{
        public class EventsPreviewDto
    {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ShortContent { get; set; }
            public string Image{ get; set; }
            public DateTime CreationDate { get; set; }
            public ICollection<Comment> Comments { get; set; }
            public ICollection<EventReaction> Reactions { get; set; }
            public int CommentsCount { get; set; }
            public int ReactionsCount { get; set; }
            public string CreatedBy { get; set; }

            public EventsPreviewDto()
            {
                Comments = new List<Comment>();
                Reactions = new List<EventReaction>();
            }


        }
}


