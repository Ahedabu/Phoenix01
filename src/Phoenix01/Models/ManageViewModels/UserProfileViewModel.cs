using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phoenix01.Models.ManageViewModels
{
    public class UserProfileViewModel
    {
        public string RegistrationDate { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }
        
        public string BirthDate { get; set; }

        public string UserAge { get; set; }

        public string Zip { get; set; }

        public string StreetName { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }


        [Display(Name = "UserImage")]
        public string UserImage { get; set; }
        
        public string RemoveUserLanguage { get; set; }

        public string AddUserLanguage { get; set; }

        public List<Hobby> ChosenHobbies { get; set; }

        public List<Language> ChosenLanguages { get; set; }

        public IEnumerable<SelectListItem> LanguagesDropDown { get; set; }

        public IEnumerable<SelectListItem> LanguagesRemoveDropDown { get; set; }
    }
}
