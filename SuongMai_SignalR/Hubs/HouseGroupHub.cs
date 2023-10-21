using Microsoft.AspNetCore.SignalR;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;

namespace SuongMai_SignalR.Hubs
{
    public class HouseGroupHub : Hub
    {
        public static List<string> GroupJoined { get; set; }= new List<string>();

        public async Task JoinHouse(string houseName)
        {
            if (!GroupJoined.Contains(Context.ConnectionId+":"+ houseName))
            {
                GroupJoined.Add(Context.ConnectionId + ":" + houseName);
                string houseList = "";
                foreach( var str in GroupJoined)
                {
                    if (str.Contains(Context.ConnectionId)) { 
                        houseList += str.Split(':')[1]+" ";
                    }
                }

                await Clients.Caller.SendAsync("suscriptionStatusb", houseList, houseName, true);
                await Clients.Others.SendAsync("newMemberAddedToHouse", houseName);
                await Groups.AddToGroupAsync(Context.ConnectionId, houseName);
            }

        }

        public async Task LeaveHouse(string houseName)
        {
            if (!GroupJoined.Contains(Context.ConnectionId + ":" + houseName))
            {
                GroupJoined.Remove(Context.ConnectionId + ":" + houseName);

                string houseList = "";
                foreach (var str in GroupJoined)
                {
                    if (str.Contains(Context.ConnectionId))
                    {
                        houseList += str.Split(':')[1] + " ";
                    }
                }
                await Clients.Caller.SendAsync("suscriptionStatusb", houseList, houseName, false);
                await Clients.Others.SendAsync("newMemberRemovedToHouse", houseName);

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, houseName);
            }

        }

        public async Task TriggerHouseNotify(string houseName)
        {
            Clients.Group(houseName).SendAsync("triggerHouseNotification", houseName);
        }


    }
}
