using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.ViewModels
{
	//TO DO IMAGE FOR COMMENT
	public class CommentViewModel
	{
		public Guid AuthorId { get; set; }
		public int CommentId { get; set; }
		public int EventId { get; set; }
		public string Content { get; set; }
	}
}
