using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class PrivateChatsViewModel
    {
        public IEnumerable<PrivateChat> PrivateChatList { get; set; }
        public PrivateChat PrivatChat { get; set; }
        public ApplicationUser UserA { get; set; }
        public ApplicationUser UserB { get; set; }
        public string PrivateChatMessage { get; set; }
        public string TimeStamp { get; set; }

    }
}
