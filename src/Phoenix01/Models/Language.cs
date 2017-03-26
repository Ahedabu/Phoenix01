using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class Language
    {
        public int ID { get; set; }
        public string LanguageName { get; set; }

        public ICollection<UserLanguage> UserLanguages { get; set; }
    }
}
