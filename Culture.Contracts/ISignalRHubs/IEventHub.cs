using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Culture.Contracts.ISignalRHubs
{
    public interface IEventHub
    {
        Task OnDisconnectedAsync(Exception exception);
        Task OnConnectedAsync();
    }
}
