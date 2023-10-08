using Microsoft.AspNetCore.SignalR;

namespace SuongMai_SignalR.Hubs
{
    public class DeathlyHallowsHub :Hub
    {
        public Dictionary<string, int> GetRaceStatus()
        {
            return SD.DealthyHallowRace;
        }

    }
}
