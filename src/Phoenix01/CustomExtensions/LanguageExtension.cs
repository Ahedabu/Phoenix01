using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phoenix01.Data;
using Phoenix01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.CustomExtensions
{
    public static class LanguageExtension
    {
        public static IEnumerable<SelectListItem> ToSelectLanguageListItems(this IEnumerable<Language> languages, IEnumerable<ApplicationUserLanguage> applicationUserLanguage, ApplicationUser user)
        {
            var languageList = languages
                .OrderBy(lang => lang.Name)
                .Where(lang => !applicationUserLanguage.Any(au => au.LanguageId == lang.Id && au.ApplicationUserId == user.Id))
                .Select(la => new SelectListItem
                {
                    Text = la.Name,
                    Value = la.Id.ToString()
                });

            return languageList;
        }

        public static IEnumerable<SelectListItem> ToRemoveLanguageListItems(this IEnumerable<Language> languages, IEnumerable<ApplicationUserLanguage> applicationUserLanguage, ApplicationUser user)
        {
            var languageList = languages
                .OrderBy(lang => lang.Name)
                .Where(lang => applicationUserLanguage.Any(au => au.LanguageId == lang.Id && au.ApplicationUserId == user.Id))
                .Select(la => new SelectListItem
                {
                    Text = la.Name,
                    Value = la.Id.ToString()
                });


            return languageList;
        }

        public static List<Language> ToPresentLanguageListItems(this IEnumerable<Language> languages, IEnumerable<ApplicationUserLanguage> applicationUserLanguage, ApplicationUser user)
        {
            var languageList = languages
                .OrderBy(lang => lang.Name)
                .Where(lang => applicationUserLanguage.Any(au => au.LanguageId == lang.Id && au.ApplicationUserId == user.Id)).ToList();
            return languageList;
        }
    }
}
