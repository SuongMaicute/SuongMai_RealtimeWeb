using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using SuongMai_SignalR.Data;

namespace SuongMai_SignalR.Hubs
{
    public class BasicChatHub : Hub
    {
        private readonly ApplicationDbContext _db;
        public BasicChatHub(ApplicationDbContext db)
        {
            _db = db;   
        }

        public async Task SendMessageToAll (string user,string message)
        {
            await Clients.All.SendAsync ("MessageReceived",user, message);
        }

        [Authorize]
        public async Task SendMessageToReceiver(string sender, string receiver, string message)
        {
            var UserID = _db.Users.FirstOrDefault(u => u.Email.ToLower() == receiver.ToLower()).Id;
            if(!string.IsNullOrEmpty(UserID) )
            {
                await Clients.User(UserID).SendAsync("MessageReceived",sender, message);
            }
        }

    }
}
