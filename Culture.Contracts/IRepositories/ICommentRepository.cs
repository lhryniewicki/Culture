using Culture.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.IRepositories
{
	public interface ICommentRepository
	{
		Task AddComentAsync(Comment comment);
		Task<Comment> GetCommentAsync(int id);
	}
}
