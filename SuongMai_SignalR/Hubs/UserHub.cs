using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SuongMai_SignalR.Hubs
{
    public class UserHub :Hub
    {
        public static int totalView { get; set; } = 0;

        public async Task NewWindownLoaded()
        {
            totalView++;
            // send update to all client 
            await Clients.All.SendAsync("updateTotalViews",totalView);
        }



    }
}
