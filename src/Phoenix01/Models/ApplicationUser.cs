using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Phoenix01.Models
{

    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser() {
            Stories = new List<Story>();
        }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RegistrationDate { get; set; }

        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }
        public string MiddleName { get; set; }

        public DateTime? BirthDate { get; set; }
        public string LastName { get; set; }
        public string Zip { get; set; }
        public string StreetName { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string UserImage { get; set; }

        // nav. prop.
        public List<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
        public List<ApplicationUserHobby> ApplicationUserHobby { get; set; }

        public string Image { get; set; }
        public string Story { get; set; }

        public List<Story> Stories  { get; set; }
    }
}
