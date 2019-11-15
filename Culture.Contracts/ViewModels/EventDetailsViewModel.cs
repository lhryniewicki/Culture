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
        public string AuthorAvatar { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime TakesPlaceDate { get; set; }
        public string CityName { get; set; }
        public string StreetName { get; set; }
        public int Price { get; set; }
        public IEnumerable<CommentDto> Comments { get; set; }
        public IEnumerable<EventReactionDto> Reactions { get; set; }
        public IEnumerable<RecommendedEventDto> RecommendedEvents{ get; set; }
        public int CommentsCount { get; set; }
        public int ReactionsCount { get; set; }
        public string CreatedBy { get; set; }
        public string CurrentReaction { get; set; }
        public string Category { get; set; }
        public bool CanLoadMore { get; set; }
        public bool IsUserSigned { get; set; }
        public Guid AuthorId { get; set; }
        public bool IsInCalendar { get; set; }
        public int ParticipantsNumber { get; set; }
        public EventDetailsViewModel(EventDetailsDto e)
        {
            Id = e.Id;
            Name = e.Name;
            Content = e.Content;
            Image = e.Image;
            CreationDate = e.CreationDate;
            TakesPlaceDate = e.TakesPlaceDate;
            CreatedBy = e.CreatedBy;
            Category = e.Category;
            CityName = e.CityName;
            StreetName = e.StreetName;
            Price = e.Price;
            IsInCalendar = e.IsInCalendar;
        }

        public EventDetailsViewModel(EventDetailsDto eventDto, MoreCommentsDto commentsDto, EventReactionsDto reactions, bool isUserAttending, IEnumerable<RecommendedEventDto> recommendedEvents)
        {
            Id = eventDto.Id;
            Name = eventDto.Name;
            Content = eventDto.Content;
            AuthorAvatar = eventDto.AuthorAvatarPath;
            Image = eventDto.Image;
            Category = eventDto.Category;
            CityName = eventDto.CityName;
            StreetName = eventDto.StreetName;
            Price = eventDto.Price;
            IsInCalendar = eventDto.IsInCalendar;
            IsUserSigned = isUserAttending;
            CreationDate = eventDto.CreationDate;
            TakesPlaceDate = eventDto.TakesPlaceDate;
            CreatedBy = eventDto.CreatedBy;
            CanLoadMore = commentsDto.CanLoadMore;
            Comments = commentsDto.CommentsList;
            CommentsCount = commentsDto.TotalCount;
            Reactions = reactions.EventReactions;
            ReactionsCount = reactions.TotalCount;
            CurrentReaction = reactions.CurrentReaction;
            RecommendedEvents = recommendedEvents;
            ParticipantsNumber = eventDto.ParticipantsNumber;
            AuthorId = eventDto.AuthorId;
        }
    }
}
