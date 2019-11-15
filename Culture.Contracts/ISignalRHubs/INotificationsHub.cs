using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Culture.Contracts.ISignalRHubs
{
    public interface INotificationsHub
    {
        Task Send(IReadOnlyList<string> userIds, string message);
        Task OnDisconnectedAsync(Exception exception);
        Task OnConnectedAsync();
        Task ReceiveNotification(string message);
    }
}
