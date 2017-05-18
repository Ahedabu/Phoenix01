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
using static Phoenix01.Models.Comment;

namespace Phoenix01.Controllers
{
    public class CommentController : Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;



        public CommentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

       

        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            Story s = new Story();
            var model = await _context.Comments
               .Include(a => a.applicationUser)
               .Where(p => p.StoryId == s.ID)
               .Select(u =>
               new StoriesViewModel
               {
                   ID = u.StoryId,
                   Title = u.story.Title,
                   StoryBody = u.story.StoryBody,
                   ApplicationUser = u.applicationUser,
                   LoggedInUser = user,
                   Comments = _context.Comments.Where(z => z.StoryId == s.ID).ToList()
               }).ToListAsync();

            return View(model);
        }


        public IActionResult Create(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Comment comment = _context.Comments.FirstOrDefault(m => m.id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }




        [HttpPost]
        public async Task<IActionResult> Create(Comment newComment)
        {

            var user = await GetCurrentUserAsync();
            Story story = new Story();

            if (User.Identity.IsAuthenticated)
            {
                story = _context.Stories
                    .Include(p => p.Comments)
                    .Include(p => p.ApplicationUser)
                    .Where(p => p.ID == newComment.StoryId)
                    .FirstOrDefault();

                newComment.CreatedDate = DateTime.Now;
                newComment.ApplicationUserId = user.Id;
                _context.Comments.Add(newComment);
                await _context.SaveChangesAsync();

            }


            if (!this.ModelState.IsValid)
            {
                return View("Details", story);
            }

         
           return RedirectToAction("index", "Stories");
        }






        // GET: Comment/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment comment = _context.Comments.Single(m => m.id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comment/Reply/5
        public IActionResult Reply(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IEnumerable<Comment> context = _context.Comments.ToList().Where(m => m.StoryId == id);

            if (context.Count() <= 0)
            {
                return NotFound();
            }

            return View(context);
        }

        // GET: Comment/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment comment = _context.Comments.Single(m => m.id == id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Update(comment);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comment/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment comment = _context.Comments.Single(m => m.id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Comment comment = _context.Comments.Single(m => m.id == id);
            _context.Comments.Remove(comment);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

      


    }
}
