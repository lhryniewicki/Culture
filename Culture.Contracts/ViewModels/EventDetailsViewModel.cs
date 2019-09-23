using Culture.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.ViewModels
{
    public class EventDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime TakesPlaceDate { get; set; }
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
        public EventDetailsViewModel(EventDetailsDto e)
        {
            Id = e.Id;
            Name = e.Name;
            Content = e.Content;
            Image = e.Image;
            CreationDate = e.CreationDate;
            TakesPlaceDate = e.TakesPlaceDate;
            CanLoadMore = e.CanLoadMore;
            Comments = e.Comments;
            CommentsCount = e.CommentsCount;
            Reactions = e.Reactions;
            ReactionsCount = e.ReactionsCount;
            CreatedBy = e.CreatedBy;
            CurrentReaction = e.CurrentReaction;
            Category = e.Category;
            CityName = e.CityName;
            StreetName = e.StreetName;
            Price = e.Price;
        }
    }
}
