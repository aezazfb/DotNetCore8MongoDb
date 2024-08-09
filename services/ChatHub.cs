using Microsoft.AspNetCore.SignalR;

namespace testProjectApis.services
{
    public sealed class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} is connected!");
        }
    }
}
