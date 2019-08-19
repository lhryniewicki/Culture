using Culture.Contracts.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts
{
    public interface IUnitOfWork
    {
        ICommentRepository CommentRepository { get;}
        IEventRepository EventRepository { get; }
        IUserRepository UserRepository { get;}
        INotificationRepository NotificationRepository { get; }

       Task Commit();

    }
}
