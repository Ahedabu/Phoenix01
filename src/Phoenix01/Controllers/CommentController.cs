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
    public class CommentController :Controller
    {

        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;



        public CommentController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public ViewResult Index()
        {
            var comment = _context.Comments.Include(c => c.applicationUser);
            return View(comment.ToList());
        }

        public PartialViewResult Create(int StoryID)
        {
            Comment newComment = new Comment();
            newComment.ParentId = StoryID;
            return PartialView(newComment);
        }



        //public async Task<IActionResult> Create([Bind("id,comment,CreatedDate,ParentId,ApplicationUserId")] Comment comment)
        //{

        //    var user = await GetCurrentUserAsync();

        //    if (ModelState.IsValid)
        //    {

        //        var userComments = new Comment {id =comment.id,comment =comment.comment,CreatedDate = DateTime.Now,ParentId = Story. ,ApplicationUserId =user.Id };
        //        _context.Add(comment);
        //        await _context.SaveChangesAsync();
                            

        //    }

        //    return RedirectToAction("Details", "Comment", new { id = comment.ParentId });
        //}


        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        //private Task<Story> GetCurrentStoryAsync()
        //{

        //    return 
        //}


    }
}
