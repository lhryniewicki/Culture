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
    public class NotificationRepository :INotificationRepository
    {
        private readonly CultureDbContext _dbContext;

        public NotificationRepository(CultureDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task CreateNotification(Notification notification)
        {
            return _dbContext.Notifications.AddAsync(notification);
        }

        public Task<Notification> GetNotificationAsync(int id)
        {
            return _dbContext.Notifications.
                SingleOrDefaultAsync(x => x.Id == id);
        }
        public Task<Notification> GetNotificationsForUserAsync(int id,int skip=0, int take=5)
        {
            return _dbContext.Notifications.
                Skip(skip*take).
                Take(take).
                SingleOrDefaultAsync(x => x.Id == id && x.IsRead==false);
        }
        public Task<int> GetNumberOfUnreadNotifications(Guid userId)
        {
            return _dbContext.Notifications
                .Where(x=>x.IsRead==true && x.UserId==userId )
                .CountAsync();
        }
    }
}
