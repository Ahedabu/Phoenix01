using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class Language
    {
        public int ID { get; set; }
        public string Name { get; set; }

        // nav. prop.
        public List<ApplicationUserLanguage> UserLinks { get; set; }
    }
}
