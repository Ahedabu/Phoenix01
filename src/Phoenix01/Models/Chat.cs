using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string ChatMessage { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }
}
