using Culture.Contracts.DTOs;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Culture.Contracts.ViewModels
{
    public class CommentsListViewModel
    {

        public CommentsListViewModel(IEnumerable<CommentDto> comments)
        {
            CommentsList = comments;
        }

        public IEnumerable<CommentDto> CommentsList{ get; set; }
    }
}
