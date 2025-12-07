using Microsoft.AspNetCore.SignalR;

namespace ShopTARge24.Hubs
{
    public class ChatHub : Hub
    {
        private static List<string> Messages = new List<string>();

        public override async Task OnConnectedAsync()
        {
            await Clients.Caller.SendAsync("ReceiveMessages", Messages);
            await base.OnConnectedAsync();
        }

        public async Task SendMessage(string user, string message)
        {
            var formattedMessage = $"{user}: {message}";
            Messages.Add(formattedMessage);

            await Clients.All.SendAsync("ReceiveMessage", formattedMessage);
        }
    }
    
}
