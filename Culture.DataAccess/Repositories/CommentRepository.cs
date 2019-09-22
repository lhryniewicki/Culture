using Culture.Contracts.IRepositories;
using Culture.DataAccess.Context;
using Culture.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Culture.DataAccess.Repositories
{
	public class CommentRepository : ICommentRepository
	{
		private readonly CultureDbContext _dbContext;

		public CommentRepository(CultureDbContext dbContext)
		{
			_dbContext = dbContext;
		}
		public async Task AddCommentAsync(Comment comment)
		{
			await _dbContext.Comments.AddAsync(comment);
		}

		public Task<Comment> GetCommentAsync(int id)
		{
			return _dbContext.Comments
				.SingleOrDefaultAsync(x => x.Id == id);
		}
        
        public async Task<IEnumerable<Comment>> GetEventCommentsAsync(int id, int page, int take)
        {
            return await _dbContext.Comments
                .Include(x => x.Author)
                .Where(x => x.EventId == id)
                .OrderByDescending(x=>x.CreationDate)
                .Skip(page * take)
                .Take(take+1)
                .ToListAsync();
        }
        public void  DeleteComment(Comment comment)
        {
             _dbContext.Comments.Remove(comment);
        }
    }
}
