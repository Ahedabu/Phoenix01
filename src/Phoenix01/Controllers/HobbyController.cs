using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Phoenix01.Data;
using Phoenix01.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phoenix01.Data.Managers;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Phoenix01.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> Edit()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new EditHobbiesViewModel()
            {
                Id = user.Id
            };

            var allHobbies = _context.Hobbies.OrderBy(h => h.Name).ToList();
            var userHobbies = _context.Hobbies
                .Where(h => _context.ApplicationUserHobbies.Any(uh => uh.HobbyId == h.Id && uh.ApplicationUserId == user.Id))
                .ToList();

            var checkBoxListItems = new List<CheckBoxListItem>();

            foreach (var hobby in allHobbies)
            {
                checkBoxListItems.Add(new CheckBoxListItem()
                {
                    Id = hobby.Id,
                    Display = hobby.Name,
                    //We should have already-selected genres be checked
                    IsChecked = userHobbies.Where(x => x.Id == hobby.Id).Any()
                });
            }

            model.SelectedHobbies = checkBoxListItems;
            //model.Hobbies = allHobbies;
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditHobbiesViewModel model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var selectedHobbies = model.SelectedHobbies.Where(x => x.IsChecked).Select(x => x.Id).ToList();

            _context.ApplicationUserHobbies.RemoveRange(_context.ApplicationUserHobbies.Where(a => a.ApplicationUserId == user.Id));
            await _context.SaveChangesAsync();

            foreach (var hobbyId in selectedHobbies)
            {
                var hobby = _context.Hobbies.FirstOrDefault(h => h.Id == hobbyId);
                _context.ApplicationUserHobbies.Add(new ApplicationUserHobby { ApplicationUserId = user.Id, HobbyId = hobby.Id });
            }

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Edit");

        }
    }
}

