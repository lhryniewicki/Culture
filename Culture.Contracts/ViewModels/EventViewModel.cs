using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Culture.Contracts.ViewModels
{
	public class EventViewModel
	{
        public int Id { get; set; }
        [Required]
        public Guid AuthorId { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public IFormFile Image { get; set; }
        [Required]
        public string Category { get; set; } //zrobic enum        
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string CityName { get; set; }
        [Required]
        public string[] EventDate { get; set; }
        [Required]
        public string EventTime { get; set; }
        public List<ReactionViewModel> Reactions { get; set; }
	}
}
