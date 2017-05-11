using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix01.Models;

namespace Phoenix01.Data.Managers
{
    public static class UserManager
    {


        public static IEnumerable<ApplicationUser> FilterUsers(this IEnumerable<ApplicationUser> users, IEnumerable<ApplicationUserLanguage> applicationUserLanguages, IEnumerable<ApplicationUserHobby> applicationUserHobbies, Agegroups? group = null, Language lang = null, Hobby hobby = null)
        {
            var ageGroup = AgeSwitcher(group);
            IEnumerable<ApplicationUser> userList = null;

            if (group == null)
                userList = users.OrderBy(user => user.LastName);

            if (group != Agegroups.SeventysixAndUp && group != null)
            {
                userList = users
                   .OrderBy(user => user.LastName)
                   .Where(user => user.BirthDate.AddYears(ageGroup) >= DateTime.Now && user.BirthDate.AddYears(ageGroup - 10) < DateTime.Now);
            }

            if(group == Agegroups.SeventysixAndUp)
            {
                userList = users
                       .OrderBy(user => user.LastName)
                       .Where(user => user.BirthDate.AddYears(ageGroup) > DateTime.Now);
            }

            if (lang != null)
            {
                userList = userList
                    .Where(user => applicationUserLanguages.Any(au => au.LanguageId == lang.Id && au.ApplicationUserId == user.Id));

            }

            if (hobby != null)
            {
                userList = userList
                    .Where(user => applicationUserHobbies.Any(ah => ah.HobbyId == hobby.Id && ah.ApplicationUserId == user.Id));
            }
            return userList;
        }

        public static IEnumerable<SelectListItem> ToAgeGroupDropDown()
        {
            var hobbieList = new List<SelectListItem>();
            hobbieList.Add(new SelectListItem
            {
                Text = "0 to 25",
                Value = "ZeroToTwentyfive"
            });
            hobbieList.Add(new SelectListItem
            {
                Text = "26 to 35",
                Value = "TwentysixToThirtyfive"
            });
            hobbieList.Add(new SelectListItem
            {
                Text = "36 to 45",
                Value = "ThirtysixToFortyfive"
            });
            hobbieList.Add(new SelectListItem
            {
                Text = "46 to 55",
                Value = "FortysixToFiftyfive"
            });
            hobbieList.Add(new SelectListItem
            {
                Text = "56 to 65",
                Value = "FiftysixToSixtyfive"
            });
            hobbieList.Add(new SelectListItem
            {
                Text = "66 to 75",
                Value = "SixtysixToSeventyfive"
            }); hobbieList.Add(new SelectListItem
            {
                Text = "76 and up",
                Value = "SeventysixAndUp"
            });

            return hobbieList;
        }


        #region
        public static int AgeSwitcher(Agegroups? group)
        {

            switch (group)
            {
                case Agegroups.ZeroToTwentyfive:
                    return (25);
                case Agegroups.TwentysixToThirtyfive:
                    return (35);
                case Agegroups.ThirtysixToFortyfive:
                    return (45);
                case Agegroups.FortysixToFiftyfive:
                    return (55);
                case Agegroups.FiftysixToSixtyfive:
                    return (65);
                case Agegroups.SixtysixToSeventyfive:
                    return (75);
                case Agegroups.SeventysixAndUp:
                    return (75);
                default:
                    return 0;
            }
        }


        public enum Agegroups
        {
            ZeroToTwentyfive,
            TwentysixToThirtyfive,
            ThirtysixToFortyfive,
            FortysixToFiftyfive,
            FiftysixToSixtyfive,
            SixtysixToSeventyfive,
            SeventysixAndUp
        }

        public enum Languages
        {
            Arabic,
            English,
            Hungarian,
            Swedish
        }

        public enum Hobbies
        {
            Food,
            Sport,
            Music,
            Film,
            Litterature
        }



        #endregion
    }
}
