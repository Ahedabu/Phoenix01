using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class ApplicationUserHobby
    {
        public string ApplicationUserId { get; set; }
        public int HobbyId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public Hobby Hobby { get; set; }

    }
}
