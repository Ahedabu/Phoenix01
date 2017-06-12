using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class ChatsViewModel
    {
        public IEnumerable<Chat> ChatList { get; set; }
        public Chat Chat { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string ChatMessage { get; set; }
        public string TimeStamp { get; set; }
    }
}
