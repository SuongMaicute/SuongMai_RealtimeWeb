using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace SuongMai_SignalR.Hubs
{
    public class NotificationHub : Hub 
    {
        public static int notificationCounter = 0;
        public static List<string> messages = new ();

        public async Task SendMessage (string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                notificationCounter++;
                messages.Add(message);
                await LoadMessage();
            }

        }

        public async Task LoadMessage()
        {
            await Clients.All.SendAsync("LoadNotification",messages,notificationCounter);
        }
    }
}
