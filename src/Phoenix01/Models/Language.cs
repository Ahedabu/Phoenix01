using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class Language
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool MotherTounge { get; set; }
        // nav. prop.
        public List<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
    }
}
