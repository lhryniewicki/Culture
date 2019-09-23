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
            public IEnumerable<EventReactionDto> Reactions { get; set; }
            public int CommentsCount { get; set; }
            public int ReactionsCount { get; set; }
            public string CreatedBy { get; set; }
            public string CurrentReaction { get; set; }
            public bool CanLoadMore { get; set; }
            public string UrlSlug { get; set; }

        public EventsPreviewDto()
            {
                Comments = new List<CommentDto>();
                Reactions = new List<EventReactionDto>();
            }

        public EventsPreviewDto(Event e,int size,IEnumerable<EventReaction> userReactions)
        {
            CreatedBy = e.CreatedBy.UserName;
            Comments = e.Comments.OrderByDescending(x=>x.CreationDate).Take(size + 1).Select(y => new CommentDto(y));
            CanLoadMore = Comments.Count() == (size+1) ? true : false;
            Comments = Comments.Count()>5?Comments.Take(size):Comments;
            CreationDate = e.CreationDate;
            Image = e.ImagePath;
            Reactions = e.Reactions.GroupBy(x=>x.Type).Select(x=>new EventReactionDto() {
                Count=x.Count(),
                ReactionType=x.Key.ToString().ToLower()
            }).OrderByDescending(x=>x.Count);
            ReactionsCount = e.Reactions.Count;
            Name = e.Name;
            CommentsCount = e.Comments.Count;
            ReactionsCount = e.Reactions.Count;
            ShortContent = e.Content.Substring(0, e.Content.Length > 255 ? 255 : e.Content.Length);
            Id = e.Id;
            CurrentReaction = userReactions.Where(x => x.EventId == e.Id).Select(x => x.Type.ToString().ToLower()).FirstOrDefault();
            UrlSlug = e.UrlSlug;
        }
    }
}


