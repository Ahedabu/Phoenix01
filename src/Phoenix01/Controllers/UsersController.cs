using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix01.Data;
using Phoenix01.Models;
using Phoenix01.Models.ManageViewModels;
using Phoenix01.CustomExtensions;

namespace Phoenix01.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Users
        public ActionResult Index()
        {
            
        

                var users = _context.ApplicationUser
                .OrderBy(u => u.UserName)
                .Select(u =>
            new UserProfileViewModel
            {
                RegistrationDate = u.RegistrationDate.ToString("yyyy-MM-dd"),
                FirstName = u.FirstName,
                MiddleName = u.MiddleName,
                LastName = u.LastName,
                StreetName = u.StreetName,
                Zip = u.Zip,
                State = u.State,
                City = u.City,
                Country = u.Country,
                UserImage = u.UserImage,
                BirthDate = u.BirthDate.ToString(),
                UserAge = CalculateAge(u)



            }).ToList();
            return View(users);
        }
        

        public int CalculateAge(ApplicationUser user)
    {
    
        var age = 0;
        var birthdate = "";
        if (user.BirthDate != null)
        {
            birthdate = ((DateTime)user.BirthDate).ToString("yyyy-MM-dd");
            age = DateTime.Today.Year - ((DateTime)user.BirthDate).Year;
            if (DateTime.Today < ((DateTime)user.BirthDate).AddYears(age)) age--;
        }



        return age;

    }
       

       
     

       

       
       

      
    }
}
