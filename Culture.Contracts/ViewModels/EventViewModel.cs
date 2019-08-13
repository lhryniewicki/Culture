using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Culture.Contracts.ViewModels
{
	public class EventViewModel
	{
        public Guid AuthorId { get; set; }
        public int Id { get; set; }

        public int Price { get; set; }
		public string Name { get; set; }
		public string Content { get; set; }
		public byte[] Image { get; set; }
		public string Category { get; set; } //zrobic enum
		public string StreetName { get; set; }
		public string CityName { get; set; }
	}
}
