using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phoenix01.Models.ManageViewModels
{
    public class EditUserProfileViewModel
    {
        public string RegistrationDate { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Zip { get; set; }

        public string StreetName { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Image { get; set; }

        public string NativeLanguage { get; set; }

        public string OtherLanguages { get; set; }

        public string AddLanguage { get; set;}

        public IEnumerable<SelectListItem> LanguagesDropDown { get; set; }
    }
}
