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
        public string CreatedBy { get; set; }
        public string AuthorAvatarPath { get; set; }
        public string Category { get; set; }
        public bool IsInCalendar { get; set; }
        public int ParticipantsNumber { get; set; }
        public Guid AuthorId { get; set; }


        public EventDetailsDto(Event e)
        {
            Id = e.Id;
            Name = e.Name;
            Content = e.Content;
            Image = e.ImagePath;
            CreationDate = e.CreationDate;
            TakesPlaceDate = e.TakesPlaceDate;
            CreatedBy = e.CreatedBy.UserName;
            AuthorAvatarPath = e.CreatedBy.AvatarPath;
            Category = e.Category;
            CityName = e.CityName;
            StreetName = e.StreetName;
            Price = e.Price;
            ParticipantsNumber = e.Participants.Count;
            AuthorId = e.CreatedById;
        }
        public EventDetailsDto()
        {

        }
    }
}
