using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phoenix01.Data;
using Phoenix01.Models;

namespace Phoenix01.Controllers
{
    public class ChatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ChatController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Chat
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            if (user == null)
            {
                return View("Error");
            }

            var model = await GetIndexFullAndPartial();
            return View(model);
        }

        [HttpGet]
        public async Task<ChatsViewModel> GetIndexFullAndPartial()
        {
            var model = new ChatsViewModel();

            var chats = await _context.Chats
            .OrderBy(c => c.TimeStamp)
            .Select(c =>
            new Chat
            {
                TimeStamp = c.TimeStamp,
                ChatMessage = c.ChatMessage,
                ApplicationUser = c.ApplicationUser
            }).ToListAsync();

            model.ChatList = chats;


            return model;
        }

        [HttpPost]
        public async Task<IActionResult> Index(ChatsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await GetCurrentUserAsync();
                if (user != null)
                {
                    if (model.ChatMessage != null)
                    {
                        var chatMessage = new Chat { TimeStamp = DateTime.Now, ChatMessage = model.ChatMessage, ApplicationUser = user };


                        _context.Add(chatMessage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> UpdateChat()
        {
            var model = await GetIndexFullAndPartial();

            return PartialView("ChatWindowPartial", model);
        }

        #region
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }



        #endregion

    }
}
