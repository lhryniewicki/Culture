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
            public Guid CreatedById { get; set; }
            public string CurrentReaction { get; set; }
            public bool CanLoadMore { get; set; }
            public string UrlSlug { get; set; }

        public EventsPreviewDto()
            {
                Comments = new List<CommentDto>();
                Reactions = new List<EventReactionDto>();
            }

        public EventsPreviewDto(Event e,MoreCommentsDto moreCommentsDto)
        {
            CreatedBy = e.CreatedBy.UserName;
            CreationDate = e.CreationDate;
            Image = e.ImagePath;
            Name = e.Name;
            ShortContent = e.Content.Substring(0, e.Content.Length > 255 ? 255 : e.Content.Length);
            Id = e.Id;
            UrlSlug = e.UrlSlug;
            CanLoadMore = moreCommentsDto.CanLoadMore;
            CommentsCount = moreCommentsDto.TotalCount;
            Comments = moreCommentsDto.CommentsList;
            CreatedById = e.CreatedById;
        }
    }
}


