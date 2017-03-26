using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class UserLanguages
    {
        public int ID { get; set; }
        public ApplicationUser AppLicationUser { get; set; }
        public Languages NativeLanguage { get; set; }
    }
}
