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
		Task<Comment> CreateCommentAsync(string content, int eventId, Guid userId);
		Task<Comment> GetCommentAsync(int id);
		Task EditCommentAsync(CommentViewModel comment, Guid id);
	}
}
