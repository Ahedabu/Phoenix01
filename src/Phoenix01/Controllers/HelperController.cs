using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phoenix01.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Controllers
{


    public class HelperController : Controller
    {
        public readonly ApplicationDbContext _context;

        public HelperController(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SelectListItem> LanguageDropdown()
        {

            var languages = new List<SelectListItem>();
            foreach (var lang in _context.Languages)
            {
                languages.Add(new SelectListItem
                {
                    Text = lang.NativeLanguage,
                    Value = lang.NativeLanguage
                });
            }
            return languages;


        }
    }
}
