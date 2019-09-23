using Culture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class EventDetailsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime TakesPlaceDate{ get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public int Price { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
        public IEnumerable<EventReactionDto> Reactions { get; set; }
        public int CommentsCount { get; set; }
        public int ReactionsCount { get; set; }
        public string CreatedBy { get; set; }
        public string CurrentReaction { get; set; }
        public string Category { get; set; }
        public bool CanLoadMore { get; set; }
        public EventDetailsDto(Event e, IEnumerable<EventReaction> userReactions, int size=5)
        {
            Id = e.Id;
            Name = e.Name;
            Content = e.Content;
            Image = e.ImagePath;
            CreationDate = e.CreationDate;
            TakesPlaceDate = e.TakesPlaceDate;
            Comments = e.Comments.OrderByDescending(x => x.CreationDate).Take(size + 1).Select(y => new CommentDto(y));
            CanLoadMore = Comments.Count() == (size + 1) ? true : false;
            Comments = Comments.Count() > 5 ? Comments.Take(size) : Comments;
            CommentsCount = e.Comments.Count;
            Reactions = e.Reactions.GroupBy(x => x.Type).Select(x => new EventReactionDto()
            {
                Count = x.Count(),
                ReactionType = x.Key.ToString().ToLower()
            }).OrderByDescending(x => x.Count);
            ReactionsCount = e.Reactions.Count;
            CreatedBy = e.CreatedBy.UserName;
            CurrentReaction = userReactions.Where(x => x.EventId == e.Id).Select(x => x.Type.ToString().ToLower()).FirstOrDefault();
            Category = e.Category;
            CityName = e.CityName;
            StreetName = e.StreetName;
            Price = e.Price;
        }
    }
}
