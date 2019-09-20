using Culture.Contracts;
using Culture.Contracts.DTOs;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using System;
using System.Linq;
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

		public async Task<CommentDto> CreateCommentAsync(string content, int eventId, Guid userId,string username)
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

			return new CommentDto()
            {
                 CreationDate=comment.CreationDate,
                 Content=comment.Content,
                 AuthorName=username
            };
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

        public async Task<Comment> EditCommentAsync(EditCommentViewModel comment, Guid id)
		{
			if ("b5ce53d5-978f-42bf-74da-08d73cef40dc" != id.ToString()) return null;
            //pobrac z autoryzacji id
                var _comment = await _unitOfWork.CommentRepository.GetCommentAsync(comment.CommentId);

            _comment.Content = comment.Content;

            return _comment;
			
		}

        public async Task<IEnumerable<CommentDto>> GetEventCommentsAsync(int id, int page, int take)
        {
            var comments = await _unitOfWork.CommentRepository.GetEventCommentsAsync(id, page, take);

            return  comments.Select(x=> new CommentDto(x));
        }
        public Task Commit()
        {
            return _unitOfWork.Commit();
        }
 
    }
}
