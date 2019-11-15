﻿using Culture.Contracts.DTOs;
using Culture.Contracts.ViewModels;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IServices
{
	public interface ICommentService
	{
		Task<CommentDto> CreateCommentAsync(string content, int eventId, Guid userId,string username, string imagePath);
		Task<Comment> EditCommentAsync(EditCommentViewModel comment, Guid id,IList<string> userRoles);
        Task<MoreCommentsDto> GetEventCommentsAsync(int id, int skip, int take);
        Task DeleteComment(int commentId,Guid userId,IList<string> userRoles);
        Task Commit();

    }
}
