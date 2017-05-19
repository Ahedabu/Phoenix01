using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phoenix01.Models.ManageViewModels;
using static Phoenix01.Data.Managers.UserManager;

namespace Phoenix01.Models
{
    public class UserListViewModel
    {
        public IEnumerable<UserProfileViewModel> UserList { get; set; }
        public IEnumerable<SelectListItem> HobbyDropDown { get; set; }
        public IEnumerable<SelectListItem> LanguagesDropDown { get; set; }
        public IEnumerable<SelectListItem> AgeGroupDropDown { get; set; }

        public string FilteredLanguage { get; set; }
        public string FilteredHobby { get; set; }
        public string FilteredAgeGroup { get; set; }



        public UserProfileViewModel User { get; set; }
    }
}
