using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phoenix01.Data;
using Phoenix01.Models;
using Microsoft.AspNetCore.Identity;
using Phoenix01.CustomExtensions;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Phoenix01.Controllers
{
    public class HobbyController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HobbyController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        [HttpGet]
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Hobby> hobbylist = new List<Hobby>();
            hobbylist = _context.Hobbies.ToList();

            return View(hobbylist);
        }

        [HttpPost]
        public async Task<ActionResult> Index(List<Hobby> objHobby)
        {

            var user = await GetCurrentUserAsync();
            var userHobbyList = _context.ApplicationUserHobby.ToList();

            var countChecked = 0; var countUnchecked = 0;
            for (int i = 0; i < objHobby.Count(); i++)
            {

                var appUserHobby = new ApplicationUserHobby
                {
                    ApplicationUserId = user.Id,
                    HobbyId = objHobby[i].HobbyId
                };

                if (objHobby[i].CheckboxAnswer == true && !userHobbyList.Contains(appUserHobby))
                {
                    _context.ApplicationUserHobby.Add(appUserHobby);

                    countChecked = countChecked + 1;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    //appUserHobby = _context.ApplicationUserHobby.SingleOrDefault(h => h.HobbyId == objHobby[i].HobbyId);
                    if (appUserHobby != null)
                        _context.Remove(appUserHobby);
                    await _context.SaveChangesAsync();



                    countUnchecked = countUnchecked + 1;
                }
            }

            //var result = await _userManager.UpdateAsync(user);
            //if (result.Succeeded)
            //{
            //    await _context.SaveChangesAsync();
            //}

            return View(objHobby);

        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }


    }
}

