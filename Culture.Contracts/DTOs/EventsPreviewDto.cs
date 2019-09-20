using Culture.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
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
            public IEnumerable<CommentDto> Comments { get; set; }
            public IEnumerable<EventReaction> Reactions { get; set; }
            public int CommentsCount { get; set; }
            public int ReactionsCount { get; set; }
            public string CreatedBy { get; set; }
            public bool CanLoadMore { get; set; }

            public EventsPreviewDto()
            {
                Comments = new List<CommentDto>();
                Reactions = new List<EventReaction>();
            }

        public EventsPreviewDto(Event e)
        {
            CreatedBy = e.CreatedBy.UserName;
            Comments = e.Comments.Take(5).OrderByDescending(x=>x.CreationDate).Select(y => new CommentDto()
            {
                Content = y.Content,
                AuthorName = y.Author.UserName,
                CreationDate = y.CreationDate,
            });
            CreationDate = e.CreationDate;
            Image = e.ImagePath;
            Reactions = e.Reactions;
            Name = e.Name;
            CommentsCount = e.Comments.Count;
            ReactionsCount = e.Reactions.Count;
            ShortContent = e.Content.Substring(0, e.Content.Length > 255 ? 255 : e.Content.Length);
            Id = e.Id;
            CanLoadMore = this.Comments.ToList().Count==5?true:false;
        }
    }
}


