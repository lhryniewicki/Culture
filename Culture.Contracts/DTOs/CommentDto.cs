using System;
using System.Collections.Generic;
using System.Text;
using Culture.Models;

namespace Culture.Contracts.DTOs
{
    public class CommentDto
    {
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }

        public CommentDto(Comment x)
        {
            AuthorName = x.Author.UserName;
            Content = x.Content;
            CreationDate = x.CreationDate;
        }
        public CommentDto()
        {

        }


    }
}
