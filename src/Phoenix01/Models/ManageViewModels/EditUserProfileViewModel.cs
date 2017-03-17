using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Phoenix01.Models.ManageViewModels
{
    public class EditUserProfileViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int AreaCode { get; set; }

        public string StreetName { get; set; }

        public string Area { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Image { get; set; }

    }
}
