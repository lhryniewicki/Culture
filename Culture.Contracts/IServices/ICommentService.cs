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
		Task<Comment> EditCommentAsync(CommentViewModel comment, Guid id);
        Task<IEnumerable<Comment>> GetEventCommentsAsync(int id, int skip, int take);
        Task DeleteComment(int commentId,Guid userId,IList<string> userRoles);
        Task Commit();

    }
}
