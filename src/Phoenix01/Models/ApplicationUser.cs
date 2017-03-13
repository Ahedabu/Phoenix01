using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Phoenix01.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        //Ahed:Adding a new properties 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AreaCode { get; set; }
        public string StreetName { get; set; }
        public string Area { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Image { get; set; }
        //public DateTime RegistrationDate { get; set; }
    }






}
