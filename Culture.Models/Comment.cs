using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public string Content { get; set; }
		//nullowalne zdjecie tez zdecydowac jak ma byc przechowywane
		public int EventId{ get; set; }
		public Event Event { get; set; }

		public Guid AuthorId { get; set; }
		public AppUser Author { get; set; }


	}
}
