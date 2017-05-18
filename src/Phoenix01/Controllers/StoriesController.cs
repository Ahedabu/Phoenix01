using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phoenix01.Data;
using Phoenix01.Models;
using Phoenix01.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Phoenix01.Models.ManageViewModels;

namespace Phoenix01.Controllers
{
    public class StoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public StoriesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Stories
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();

            var model = await _context.Stories
                .Include(s => s.ApplicationUser)
                .Select(u =>
                new StoriesViewModel
                {
                    ID = u.ID,
                    Title = u.Title,
                    StoryBody = u.StoryBody,
                    ApplicationUser = u.ApplicationUser,
                    LoggedInUser = user
                }).ToListAsync();


            return View(model);
        }

        // GET: Stories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Stories.SingleOrDefaultAsync(m => m.ID == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        // GET: Stories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,StoryBody,Title,Category")] Story story)
        {
            var user = await GetCurrentUserAsync();
            if (User.Identity.IsAuthenticated)

            {

                var appUserStories = new Story { ApplicationUserId = user.Id, Category = story.Category, ID = story.ID, StoryBody = story.StoryBody, Title = story.Title };
                _context.Add(appUserStories);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }

         
            return View(story);
        }




        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }



        // GET: Stories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Stories.SingleOrDefaultAsync(m => m.ID == id);
            if (story == null)
            {
                return NotFound();
            }
            return View(story);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,StoryBody,Title,Category")] Story story)
        {
            if (id != story.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(story);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StoryExists(story.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(story);
        }

        // GET: Stories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var story = await _context.Stories.SingleOrDefaultAsync(m => m.ID == id);
            if (story == null)
            {
                return NotFound();
            }

            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var story = await _context.Stories.SingleOrDefaultAsync(m => m.ID == id);
            _context.Stories.Remove(story);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool StoryExists(int id)
        {
            return _context.Stories.Any(e => e.ID == id);
        }
    }
}