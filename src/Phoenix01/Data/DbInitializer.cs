using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Phoenix01.Models;

namespace Phoenix01.Data
{
    public class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any STORY.
            if (context.Stories.Any())
            {
                return;   // DB has been seeded
            }

            var stories = new Story[]
            {
            new Story{ Title="What is a galaxy?", StoryBody="We live on a planet called Earth that is part of our solar system. But where is our solar system? It’s a small part of the Milky Way Galaxy." },
            new Story{  Title="Tyrannosaur", StoryBody="In the twilight of the Age of Dinosaurs, tyrannosaurs were the apex predators. The bipedal carnivores spanned the globe for 14 million years in the late Cretaceous era," }
            };

            foreach (Story s in stories)
            {
                context.Stories.Add(s);
            }
            context.SaveChanges();
        }
    }
    
}