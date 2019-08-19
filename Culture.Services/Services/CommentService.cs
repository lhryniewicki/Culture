using Culture.Contracts;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
	public class CommentService:ICommentService
	{
        private readonly IUnitOfWork _unitOfWork;

        public CommentService(IUnitOfWork unitOfWork)
		{
           _unitOfWork = unitOfWork;
        }

		public async Task<Comment> CreateCommentAsync(string content, int eventId, Guid userId)
		{
			var dateTime = DateTime.Now;
			var comment = new Comment()
			{
				AuthorId = userId,
				Content = content,
				CreationDate = dateTime,
				EventId = eventId

			};
			await _unitOfWork.CommentRepository.AddCommentAsync(comment);

			return comment;
		}

        public async Task DeleteComment(int commentId, Guid userId, IList<string> userRoles)
        {
            var comment = await _unitOfWork.CommentRepository.GetCommentAsync(commentId);
            if(comment.AuthorId==userId || userRoles.Contains("Admin"))
            {
                 _unitOfWork.CommentRepository.DeleteComment(comment);
            }
          
            return;
        }

        public async Task<Comment> EditCommentAsync(CommentViewModel comment, Guid id)
		{
			if (comment.AuthorId != id) return null;

			var _comment = await _unitOfWork.CommentRepository.GetCommentAsync(comment.CommentId);
			_comment.Content = comment.Content;

            return _comment;
			
		}

        public async Task<IEnumerable<Comment>> GetEventCommentsAsync(int id, int skip, int take)
        {
            return await _unitOfWork.CommentRepository.GetEventCommentsAsync(id, skip, take);
        }
        public Task Commit()
        {
            return _unitOfWork.Commit();
        }
 
    }
}
