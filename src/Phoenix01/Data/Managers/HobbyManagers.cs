using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public static IEnumerable<SelectListItem> ToHobbyDropDown(this IEnumerable<Hobby> hobbies)
        {
            var hobbieList = hobbies
                .OrderBy(hobby => hobby.Name)
                .Select(hl => new SelectListItem
                {
                    Text = hl.Name,
                    Value = hl.Name
                });

            return hobbieList;
        }

    }
}
