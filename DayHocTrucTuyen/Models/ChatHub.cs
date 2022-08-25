using DayHocTrucTuyen.Models.Entities;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace DayHocTrucTuyen.Models
{
    public class ChatHub : Hub
    {
        DayHocTrucTuyenContext db = new DayHocTrucTuyenContext();
        static List<UserConnect> ConnectedUsers = new List<UserConnect>();

        public async Task SendToUser(string ma, string mess)
        {
            var gui = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            var nhan = ConnectedUsers.FirstOrDefault(x => x.MaNd == ma);
            if(nhan != null)
            {
                await Clients.Client(nhan.ConnectionId).SendAsync(
                    "ReceivedChat",
                    gui.MaNd,
                    gui.ImgAvt,
                    mess,
                    DateTime.Now.ToString("yyyy-MM-dd'T'HH:mm:ss")
                );
            }
        }

        public override async Task OnConnectedAsync()
        {
            var user = db.NguoiDungs.FirstOrDefault(x => x.MaNd == ((ClaimsIdentity)Context.User.Identity).Claims.First().Value);

            var id = Context.ConnectionId;
            if (ConnectedUsers.Count(x => x.ConnectionId == id) == 0)
            {
                ConnectedUsers.Add(new UserConnect { 
                    ConnectionId = id, MaNd = user.MaNd, 
                    UserName = user.getFullName(), 
                    ImgAvt = user.getImageAvt() 
                });

                //Báo có người dùng onl
                await Clients.AllExcept(Context.ConnectionId).SendAsync("UserConnect", user.MaNd);
                
                await base.OnConnectedAsync();
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (exception == null)
            {
                var item = ConnectedUsers.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);

                ConnectedUsers.Remove(item);

                //Báo có người dùng off
                await Clients.All.SendAsync("UserDisconnect", item.MaNd);
            }

            await base.OnDisconnectedAsync(exception);
        }

        public async Task CheckOnline(string username)
        {
            var user = ConnectedUsers.FirstOrDefault(x=>x.MaNd == username);

            await Clients.Client(Context.ConnectionId).SendAsync("CheckOnline", user != null ? true : false);
        }
    }
}
