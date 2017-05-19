using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Phoenix01.Models.ManageViewModels
{
    public class UserProfileViewModel
    {
        public UserProfileViewModel()
        {
            SelectedHobbies = new List<CheckBoxListItem>();
        }
        public string Id { get; set; }

        public string RegistrationDate { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string BirthDate { get; set; }

        public int UserAge { get; set; }

        public string Zip { get; set; }

        public string StreetName { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string  Email { get; set; }

        [Display(Name = "UserImage")]
        public string UserImage { get; set; }
        
        public string RemoveUserLanguage { get; set; }

        public string AddUserLanguage { get; set; }

        public List<CheckBoxListItem> SelectedHobbies { get; set; }

        public List<Hobby> ChosenHobbies { get; set; }

        public List<Language> ChosenLanguages { get; set; }

        public IEnumerable<SelectListItem> HobbyDropDown { get; set; }

        public IEnumerable<SelectListItem> LanguagesDropDown { get; set; }

        public IEnumerable<SelectListItem> AgeGroupDropDown { get; set; }

        public string AddedLanguage { get; set; }
    }
}
