using Microsoft.AspNetCore.SignalR;

namespace testProjectApis.services
{
    public class NotificationHub : Hub
    {
        //public NotificationHub() { }
        public async Task SendNotification(string message)
        {
            // Sends a message to all connected clients
            await Clients.All.SendAsync("ReceiveNotification", message);
        }

        // Optional: Override these methods to handle client connections/disconnections
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
            // You can add custom logic when a client connects, like logging
            System.Console.WriteLine($"Client connected: {Context.ConnectionId}");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
            // Custom logic when a client disconnects
            System.Console.WriteLine($"Client disconnected: {Context.ConnectionId}");
        }
    }
}
