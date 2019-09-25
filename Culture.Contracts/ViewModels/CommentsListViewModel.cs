using Culture.Contracts.DTOs;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Culture.Contracts.ViewModels
{
    public class CommentsListViewModel
    {
        public IEnumerable<CommentDto> CommentsList { get; set; }
        public bool CanLoadMore { get; set; }
        public CommentsListViewModel(MoreCommentsDto commentDtos)
        {
            CanLoadMore = commentDtos.CanLoadMore;
            CommentsList = commentDtos.CommentsList;
        }

    }
}
