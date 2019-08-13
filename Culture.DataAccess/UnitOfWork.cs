using Culture.Contracts;
using Culture.Contracts.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context.CultureDbContext _cultureDbContext;

        public ICommentRepository CommentRepository { get; }

        public IEventRepository EventRepository { get; }

        public IUserRepository UserRepository { get; }
        public UnitOfWork( 
            IUserRepository userRepository,
            IEventRepository eventRepository,
            ICommentRepository commentRepository,
            Context.CultureDbContext cultureDbContext
            )
        {
            CommentRepository = commentRepository;
            _cultureDbContext = cultureDbContext;
            UserRepository = userRepository;
            EventRepository = eventRepository;
        }

        public async Task Commit()
        {
            await _cultureDbContext.SaveChangesAsync();
        }
    }
}
