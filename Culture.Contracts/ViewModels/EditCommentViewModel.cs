using System;
using System.Collections.Generic;
using System.Text;

namespace Culture.Contracts.ViewModels
{
    public class EditCommentViewModel
    {
        public int EventId { get; set; }
        public int CommentId { get; set; }
        public string Content { get; set; }
    }
}
