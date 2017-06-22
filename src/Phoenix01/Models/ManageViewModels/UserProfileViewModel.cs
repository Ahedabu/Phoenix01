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

        [Display(Name = "Registration Date")]
        public string RegistrationDate { get; set;  }

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
        public string BirthDate { get; set; }

        [Display(Name = "Age")]
        public int UserAge { get; set; }

        [Display(Name = "Zip")]
        public string Zip { get; set; }

        [Display(Name = "Street Name")]
        public string StreetName { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Email")]
        public string  Email { get; set; }

        [Display(Name = "User Image")]
        public string UserImage { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "Add User Language")]
        public string AddUserLanguage { get; set; }

        [Display(Name = "Add User Language")]
        public string RemoveUserLanguage { get; set; }

        [Display(Name = "Chosen Hobbies")]
        public List<Hobby> ChosenHobbies { get; set; }

        [Display(Name = "Chosen Languages")]
        public List<Language> ChosenLanguages { get; set; }

        public bool IsCurrentUser { get; set; }

        public List<CheckBoxListItem> SelectedHobbies { get; set; }

        public IEnumerable<SelectListItem> HobbyDropDown { get; set; }

        public IEnumerable<SelectListItem> LanguagesDropDown { get; set; }

        public IEnumerable<SelectListItem> AgeGroupDropDown { get; set; }
    }
}
