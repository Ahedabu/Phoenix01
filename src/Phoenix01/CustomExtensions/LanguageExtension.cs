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
        public static IEnumerable<SelectListItem> ToSelectListItems(this IEnumerable<Language> languages/*, int selectedId */)
        {
            var languageList = languages.OrderBy(lang => lang.Name)
                .Select(lang =>
                new SelectListItem
                {
                    //Selected = (lang.ID == selectedId),
                    Text = lang.Name,
                    Value = lang.Name
                });

            return languageList;
        }
    }
}
