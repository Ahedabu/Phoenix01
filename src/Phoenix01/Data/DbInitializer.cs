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

            var languages = new Languages[]
            {
            new Languages {NativeLanguage="Arabic"},
            new Languages {NativeLanguage="English"},
            new Languages {NativeLanguage="Hungarian"},
            new Languages {NativeLanguage="Swedish"}
            };

            foreach (Languages lang in languages)
            {
                context.Languages.Add(lang);
            }
            context.SaveChanges();
        }
    }
}
