using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Culture.Contracts.DTOs
{
    public class MoreCommentsDto
    {
        public IEnumerable<CommentDto> CommentsList { get; set; }
        public bool CanLoadMore { get; set; }
        public int TotalCount { get; set; }
        public MoreCommentsDto(IEnumerable<CommentDto> commentDtos)
        {
            CanLoadMore = commentDtos.Count() > 5 ? true : false;
            CommentsList = commentDtos.Count() > 5 ? commentDtos.Take(5) : commentDtos;
        }
        public MoreCommentsDto()
        {

        }
    }
}
