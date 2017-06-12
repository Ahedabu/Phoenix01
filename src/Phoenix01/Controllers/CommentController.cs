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


        // GET: Comments
        public IActionResult Index()
        {
               return View();
        }


        public IActionResult Create(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Comment comment = _context.Comments.FirstOrDefault(m => m.Id == id);
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
                var appUserComments = new Comment {  StoryId= newComment.StoryId, CreatedDate = DateTime.Now,ApplicationUser = user,Content = newComment.Content};
                _context.Comments.Add(appUserComments);
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

            Comment comment = _context.Comments.Single(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "Stories");
        }

    
        // GET: Comment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,StoryId,CreatedDate")] Comment comment)
        {

            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;

                    }
                }

                return RedirectToAction("index", "Stories");
            }

            return View(comment);
   
        }


        public bool CommentExists(int id)
        {

            return _context.Comments.Any(e=>e.Id == id);

        }
        // GET: Comment/Delete/5
        [ActionName("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Comment comment = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            Comment comment = await _context.Comments.SingleOrDefaultAsync(m => m.Id == id);
            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Stories");
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

      


    }
}
