using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Culture.Contracts.ViewModels
{
	//TO DO IMAGE FOR COMMENT
	public class CommentViewModel
	{
        [Required]
		public int EventId { get; set; }

        [Required]
		public string Content { get; set; }

        public IFormFile Image { get; set; }
    }
}
