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
            ApplicationUserHobbies = new List<ApplicationUserHobby>();
        }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Registration Date")]
        public DateTime RegistrationDate { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birthdate")]
        public DateTime? BirthDate { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Display(Name = "Street")]
        public string StreetName { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "State")]
        public string State { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "City")]
        public string City { get; set; }

        [StringLength(25, MinimumLength = 2)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        public string UserImageLink { get; set; }

        // nav. prop.
        public List<ApplicationUserLanguage> ApplicationUserLanguages { get; set; }
        public List<ApplicationUserHobby> ApplicationUserHobbies { get; set; }
        public List<Story> Stories  { get; set; }
        
    }
}
