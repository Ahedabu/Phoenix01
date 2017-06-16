using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Phoenix01.Data;
using Phoenix01.Models;
using Phoenix01.Models.ManageViewModels;
using Phoenix01.Data.Managers;
using static Phoenix01.Data.Managers.UserManager;
using static Phoenix01.Data.Managers.HobbyManagers;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Phoenix01.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public UsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            var users = _context.ApplicationUser
            .OrderBy(u => u.UserName)
            

            .Select(u => 
                new UserProfileViewModel
                {
                    UserName = u.UserName,
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    Country = u.Country,
                    UserImage = u.UserImage,
                    BirthDate = u.BirthDate.ToString(),
                    Email = u.Email,
                    UserAge = CalculateAge(u)
                    

                }).ToList();

            UserListViewModel model = new UserListViewModel
            {
                UserList = users,
                LanguagesDropDown = _context.Languages.ToLanguageListItems(),
                HobbyDropDown = _context.Hobbies.ToHobbyDropDown(),
                AgeGroupDropDown = ToAgeGroupDropDown()


            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(UserListViewModel model)
        {
            Agegroups? ages;

            if (!model.FilteredAgeGroup.Contains("Select"))
                ages = (Agegroups)Enum.Parse(typeof(Agegroups), model.FilteredAgeGroup);
            else
                ages = null;

            var lang = _context.Languages.Where(l => l.Name == model.FilteredLanguage).FirstOrDefault();
            var hobby = _context.Hobbies.Where(h => h.Name == model.FilteredHobby).FirstOrDefault();

            var users = _context.ApplicationUser
            .OrderBy(u => u.UserName)
            .FilterUsers(_context.ApplicationUserLanguages, _context.ApplicationUserHobbies, ages, lang, hobby)


            .Select(u =>
                new UserProfileViewModel
                {
                    FirstName = u.FirstName,
                    MiddleName = u.MiddleName,
                    LastName = u.LastName,
                    Country = u.Country,
                    UserImage = u.UserImage,
                    BirthDate = u.BirthDate.ToString(),
                    Email = u.Email,
                    UserAge = CalculateAge(u),


                }).ToList();

            model.UserList = users;
            model.LanguagesDropDown = _context.Languages.ToLanguageListItems();
            model.HobbyDropDown = _context.Hobbies.ToHobbyDropDown();
            model.AgeGroupDropDown = ToAgeGroupDropDown();

            return View(model);
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

        #region
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
        #endregion
    }
}
