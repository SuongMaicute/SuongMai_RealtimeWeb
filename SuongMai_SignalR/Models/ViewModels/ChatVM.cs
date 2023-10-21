﻿namespace SuongMai_SignalR.Models.ViewModels
{
    public class ChatVM
    {
        public ChatVM()
        {
            Rooms = new List<ChatRoom>();
            
        }
        public int MaxRoomAllowed { get; set; }
        public IList<ChatRoom> Rooms { get; set; }

        public string? UserId { get; set; }

        public bool AllowAddRoom => Rooms == null || Rooms.Count < MaxRoomAllowed;
    }
}
