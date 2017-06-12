using System;

namespace Phoenix01.Models
{
    public class PrivateChat
    {
        public int Id { get; set; }
        public string PrivateChatMessage { get; set; }
        public DateTime TimeStamp { get; set; }

        public ApplicationUser UserA { get; set; }
        public ApplicationUser UserB { get; set; }

    }
}