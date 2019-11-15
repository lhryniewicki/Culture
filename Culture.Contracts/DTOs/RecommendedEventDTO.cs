using Culture.Models;

namespace Culture.Contracts.DTOs
{
    public class RecommendedEventDto
    {
        public string UrlSlug { get; set; }
        public string Name { get; set; }
        public string ImageSource { get; set; }

        public RecommendedEventDto(Event @event)
        {
            UrlSlug = @event.UrlSlug;
            Name = @event.Name;
            ImageSource = @event.ImagePath;
        }
    }
}
