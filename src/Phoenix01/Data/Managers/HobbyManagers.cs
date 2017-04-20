using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Phoenix01.Models;

namespace Phoenix01.Data.Managers
{

    public static class HobbyManagers
    {
        public static List<Hobby> GetAll(ApplicationDbContext context)
        {
            var hobbies = context.Hobbies.OrderBy(h => h.Name).ToList();
            return hobbies;
        }

        public static void EditUserHobbies(ApplicationUser user, ApplicationDbContext context, List<int> hobbies)
        {
            user.ApplicationUserHobbies.Clear();
            foreach (var hobbyId in hobbies)
            {
                user.ApplicationUserHobbies.Add(new ApplicationUserHobby { ApplicationUserId = user.Id, HobbyId = hobbyId});
            }
            context.SaveChanges();
        }
    }
}
