using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DayHocTrucTuyen.Models
{
    public class ChatHub : Hub
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        static List<UserConnect> ConnectedUsers = new List<UserConnect>();

        public async Task SendAll(string nick, string message)
        {
            await Clients.All.SendAsync("Send", nick, message);
        }

        public async Task SendToUser(string ma, string mess)
        {
            var user = ConnectedUsers.FirstOrDefault(x => x.MaNd == ma);
            if(user != null)
            {
                await Clients.Client(user.ConnectionId).SendAsync("Send", ma, mess);
            }
        }

        public async Task TestA()
        {
            await Clients.All.SendAsync("Send", "Tin nhắn Test", "Nội dung");
        }

        public override async Task OnConnectedAsync()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == ((ClaimsIdentity)Context.User.Identity).Claims.First().Value);
            //await Clients.AllExcept(Context.ConnectionId).SendAsync("Send", user.getName(), "Đã kết nối");

            var id = Context.ConnectionId;
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserConnect { ConnectionId = id, MaNd = user.MaNd, UserName = user.getName() });

                await Clients.AllExcept(Context.ConnectionId).SendAsync("Send", user.getName(), "Đã kết nối");
                await Clients.Client(Context.ConnectionId).SendAsync("Send", "Hiện có", ConnectedUsers.Count);
                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception == null)
            {
                var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

                ConnectedUsers.Remove(item);
                await SendAll(item.UserName, "Đã rời khỏi cuộc trò chuyện. Còn " + ConnectedUsers.Count);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
