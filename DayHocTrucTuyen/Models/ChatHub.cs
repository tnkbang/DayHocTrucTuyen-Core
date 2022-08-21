using Microsoft.AspNetCore.SignalR;

namespace DayHocTrucTuyen.Models
{
    public class ChatHub : Hub
    {
        public async Task Send(string nick, string message)
        {
            await Clients.All.SendAsync("Send", nick, message);
        }
    }
}
