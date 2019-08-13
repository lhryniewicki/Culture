using Culture.Contracts.IRepositories;
using Culture.Contracts.IServices;
using Culture.Contracts.ViewModels;
using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Services.Services
{
	class CommentService:ICommentService
	{
		private readonly ICommentRepository _commentRepository;

		public CommentService(ICommentRepository commentRepository)
		{
			_commentRepository = commentRepository;
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
			await _commentRepository.AddComentAsync(comment);
			return comment;
		}

		public async Task EditCommentAsync(CommentViewModel comment, Guid id)
		{
			if (comment.AuthorId != id) return;

			var _comment = await _commentRepository.GetCommentAsync(comment.CommentId);
			_comment.Content = comment.Content;
			
			
		}

		public Task<Comment> GetCommentAsync(int id)
		{
			throw new NotImplementedException();
		}

	}
}
