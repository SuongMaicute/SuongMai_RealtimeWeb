using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.VisualBasic;

namespace SuongMai_SignalR.Hubs
{
    public class UserHub :Hub
    {
        public static int totalView { get; set; } = 0;
        public static int totalUser { get; set; } = 0;

        public override  Task OnConnectedAsync()
        {
            totalUser++;
            Clients.All.SendAsync("updateTotalUser", totalUser).GetAwaiter().GetResult();
            return  base.OnConnectedAsync(); 
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            totalUser--;
            Clients.All.SendAsync("updateTotalUser", totalUser).GetAwaiter().GetResult();
            return base.OnDisconnectedAsync(exception);
        }

        public async Task NewWindownLoaded()
        {
            totalView++;
            // send update to all client 
            await Clients.All.SendAsync("updateTotalViews",totalView);
           

        }





    }
}
