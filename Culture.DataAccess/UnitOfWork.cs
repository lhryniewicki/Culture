using Culture.Contracts;
using Culture.Contracts.IRepositories;
using Culture.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CultureDbContext _cultureDbContext;

        public ICommentRepository CommentRepository { get; }

        public IEventRepository EventRepository { get; }

        public IUserRepository UserRepository { get; }

        public INotificationRepository NotificationRepository { get; }

		public IEventReactionRepository EventReactionRepository { get; }

		public UnitOfWork( 
            IUserRepository userRepository,
            IEventRepository eventRepository,
            ICommentRepository commentRepository,
            INotificationRepository notificationRepository,
            CultureDbContext cultureDbContext,
			IEventReactionRepository eventReactionRepository
            )
        {
            CommentRepository = commentRepository;
            _cultureDbContext = cultureDbContext;
			EventReactionRepository = eventReactionRepository;
			UserRepository = userRepository;
            EventRepository = eventRepository;
            NotificationRepository = notificationRepository;
        }

        public Task Commit()
        {
            return _cultureDbContext.SaveChangesAsync();
        }
    }
}
