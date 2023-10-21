using System.ComponentModel.DataAnnotations;

namespace SuongMai_SignalR.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
