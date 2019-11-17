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
		public async Task<CommentDto> CreateCommentAsync(string content, int eventId, Guid userId,string username,string imagePath)
		{
            var user = await _unitOfWork.UserRepository.GetUserById(userId.ToString());

			var dateTime = DateTime.Now;

			var comment = new Comment()
			{
				AuthorId = userId,
				Content = content,
				CreationDate = dateTime,
				EventId = eventId,
                ImagePath = imagePath
			};
            
			await _unitOfWork.CommentRepository.AddCommentAsync(comment);

            return new CommentDto()
            {
                CreationDate = comment.CreationDate,
                Content = comment.Content,
                AuthorName = username,
                ImagePath = imagePath,
                AvatarPath = user.AvatarPath
            };
		}

        public async Task<Comment> DeleteComment(int commentId, Guid userId, string userRole)
        {
            var comment = await _unitOfWork.CommentRepository.GetCommentAsync(commentId);

            if(comment.AuthorId == userId || userRole == "Admin")
            {
                  _unitOfWork.CommentRepository.DeleteComment(comment);
                return comment;
            }
          
            return null;
        }

        public async Task<Comment> EditCommentAsync(EditCommentViewModel comment, Guid id, string userRole)
		{
            var _comment = await _unitOfWork.CommentRepository.GetCommentAsync(comment.CommentId);

            if (id != _comment.AuthorId  && userRole != "Admin") return null;

            _comment.Content = comment.Content;

            return _comment;
		}

        public async Task<MoreCommentsDto> GetEventCommentsAsync(int id, int page, int take)
        {
            var comments = await _unitOfWork.CommentRepository.GetEventCommentsAsync(id, page, take);

            var commentsDto =  comments.Select(x=> new CommentDto(x));

            var commentsCount = await _unitOfWork.CommentRepository.GetCommentCountAsync(id);
            return new MoreCommentsDto(commentsDto,take)
            {
                TotalCount = commentsCount
            };
        }

        public Task Commit()
        {
            return _unitOfWork.Commit();
        }
 
    }
}
