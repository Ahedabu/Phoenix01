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
            if (context.Languages.Any())
            {
                return;
            }

            var languages = new Language[]
            {
            new Language {Name="Arabic"},
            new Language {Name="English"},
            new Language {Name="Hungarian"},
            new Language {Name="Swedish"}
            };

            foreach (Language lang in languages)
            {
                context.Languages.Add(lang);
            }
            context.SaveChanges();
        }
    }
}
