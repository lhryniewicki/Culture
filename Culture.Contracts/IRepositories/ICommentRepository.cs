using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
	public interface ICommentRepository
	{
		Task AddCommentAsync(Comment comment);
		Task<Comment> GetCommentAsync(int id);
        Task<IEnumerable<Comment>> GetEventCommentsAsync(int id, int skip, int take);
        void DeleteComment(Comment comment);
        Task<int> GetCommentCountAsync(int eventId);
    }
}
