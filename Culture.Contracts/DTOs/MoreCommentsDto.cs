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
        public MoreCommentsDto(IEnumerable<CommentDto> commentDtos, int sizeComments)
        {
            CanLoadMore = commentDtos.Count() > sizeComments ? true : false;
            CommentsList = commentDtos.Count() > sizeComments ? commentDtos.Take(sizeComments) : commentDtos;
        }
        public MoreCommentsDto()
        {

        }
    }
}
