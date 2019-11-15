using Culture.Contracts.ISignalRHubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Culture.Services.SignalR
{
    [Authorize]
    public class NotificationHub : Hub<INotificationsHub>
    {
        public static ConcurrentDictionary<string, string> userIdConnectionId = new ConcurrentDictionary<string, string>();
        public override async Task OnConnectedAsync()
        {
            userIdConnectionId.TryAdd(Context.User.Claims.FirstOrDefault(x => x.Type == "jti").Value, Context.ConnectionId);
            await base.OnConnectedAsync();

        }
        public async override Task OnDisconnectedAsync(Exception exception)
        {
            string value;
            userIdConnectionId.TryRemove(Context.User.Claims.FirstOrDefault(x => x.Type == "jti").Value,  out value);

            await base.OnDisconnectedAsync(exception);

        }
    }
}
