using Phoenix01.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // look for Users
            if (context.Users.Any())
            {
                return;
            }

            var languages = new Language[]
            {
            new Language {LanguageName="Arabic"},
            new Language {LanguageName="English"},
            new Language {LanguageName="Hungarian"},
            new Language {LanguageName="Swedish"}
            };

            foreach (Language lang in languages)
            {
                context.Languages.Add(lang);
            }
            context.SaveChanges();
        }
    }
}
