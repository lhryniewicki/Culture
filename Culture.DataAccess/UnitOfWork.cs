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
        public IUserInEventRepository UserInEventRepository { get; }
        public IEventInCalendarRepository EventInCalendarRepository { get; }

		public UnitOfWork( 
            IUserRepository userRepository,
            IEventRepository eventRepository,
            ICommentRepository commentRepository,
            INotificationRepository notificationRepository,
            CultureDbContext cultureDbContext,
            IEventInCalendarRepository eventInCalendarRepository,
			IEventReactionRepository eventReactionRepository,
            IUserInEventRepository userInEventRepository
            )
        {
            CommentRepository = commentRepository;
            _cultureDbContext = cultureDbContext;
            EventInCalendarRepository = eventInCalendarRepository;
            EventReactionRepository = eventReactionRepository;
            UserInEventRepository = userInEventRepository;
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
