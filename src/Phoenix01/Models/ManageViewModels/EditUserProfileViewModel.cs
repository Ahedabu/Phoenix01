using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Phoenix01.Models.ManageViewModels
{
    public class EditUserProfileViewModel
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Zip { get; set; }

        public string StreetName { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Image { get; set; }

    }
}
