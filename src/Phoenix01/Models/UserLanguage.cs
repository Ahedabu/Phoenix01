using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class UserLanguage
    {
        public int ID { get; set; }
        public Guid UserID { get; set; }

        public ApplicationUser User { get; set; }
        public Language Language { get; set; }
    }
}
