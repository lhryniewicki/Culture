using System;
using System.Collections.Generic;
using System.Text;
using Culture.Models;

namespace Culture.Contracts.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public string ImagePath { get; set; }
        public string AvatarPath { get; set; }
        public string AuthorId { get; set; }
        public CommentDto(Comment x)
        {
            Id = x.Id;
            AuthorName = x.Author.UserName;
            Content = x.Content;
            CreationDate = x.CreationDate;
            ImagePath = x.ImagePath;
            AuthorId = x.AuthorId.ToString();
            AvatarPath = x.Author.AvatarPath;
        }
        public CommentDto()
        {

        }


    }
}
