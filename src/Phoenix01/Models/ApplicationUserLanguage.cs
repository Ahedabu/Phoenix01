using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class ApplicationUserLanguage
    {
        public string ApplicationUserId { get; set; }
        public int LanguageId { get; set; }

        // nav. props.
        public ApplicationUser ApplicationUser { get; set; }
        public Language Language { get; set; }

    }
}
