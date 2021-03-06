﻿using Phoenix01.Models;
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

            context.Database.EnsureCreated();

            // Look for any STORY.
            if (context.Stories.Any())
            {
                return;   // DB has been seeded
            }

            var stories = new Story[]
            {
            new Story{Title="What is a galaxy?", StoryBody="We live on a planet called Earth that is part of our solar system. But where is our solar system? It’s a small part of the Milky Way Galaxy." },
            new Story{Title="Tyrannosaur", StoryBody="In the twilight of the Age of Dinosaurs, tyrannosaurs were the apex predators. The bipedal carnivores spanned the globe for 14 million years in the late Cretaceous era," }
            };

            foreach (Story s in stories)
            {
                context.Stories.Add(s);
            }

            if (context.Hobbies.Any())
            {
                return;
            }

            var hobbies = new Hobby[]
            {
                new Hobby { Name = "Food"},
                new Hobby { Name = "Sport"},
                new Hobby { Name= "Music"},
                new Hobby { Name = "Film"},
                new Hobby { Name = "Litterature"}
            };
            foreach (Hobby h in hobbies)
            {
                context.Hobbies.Add(h);
            }

            context.SaveChanges();

        }
    }
}