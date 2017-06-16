using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Phoenix01.Data;
using Phoenix01.Models;

namespace Phoenix01.Controllers
{
    [Authorize]
    public class PrivateChatController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public PrivateChatController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Chat
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var model = await GetIndexFullAndPartial(id);
            return View(model);
        }

        [HttpGet]
        public async Task<PrivateChatsViewModel> GetIndexFullAndPartial(string id)
        {
            var model = new PrivateChatsViewModel();

            var userB = await _context.ApplicationUser.Where(u => u.UserName == id).FirstOrDefaultAsync();
            var userA = await GetCurrentUserAsync();
            if (userA != null)
            {

                var chats = await _context.PrivateChats
            .OrderBy(c => c.TimeStamp)
            .Where(c => (c.UserA == userA && c.UserB == userB) || (c.UserA == userB && c.UserB == userA))
            .Select(c =>
            new PrivateChat
            {
                TimeStamp = c.TimeStamp,
                PrivateChatMessage = c.PrivateChatMessage,
                UserA = c.UserA,
                UserB = c.UserB
            }).ToListAsync();

                model.PrivateChatList = chats;
                model.UserA = userA;
                model.UserB = userB;

            }
            return model;
        }

        [HttpPost]
        public async Task<IActionResult> Index(PrivateChatsViewModel model, string id)
        {
            if (ModelState.IsValid)
            {
                var userA = await GetCurrentUserAsync();
                var userB = await _context.ApplicationUser.Where(u => u.UserName == id).FirstOrDefaultAsync();
                if (userA != null)
                {
                    if (model.PrivateChatMessage != null)
                    {
                        var privateChatMessage = new PrivateChat { TimeStamp = DateTime.Now, PrivateChatMessage = model.PrivateChatMessage, UserA = userA, UserB = userB };

                        _context.Add(privateChatMessage);
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> ListPrivateChats()
        {
            var userA = await GetCurrentUserAsync();

            if (userA == null)
            {
                return View("Error");
            }
            var users = _context.PrivateChats
                .OrderByDescending(c => c.TimeStamp)
                .Where(c => c.UserA == userA || c.UserB == userA)
                .Select(c => c.UserA == userA ? c.UserB : c.UserA)
                .Distinct()
                .ToList();
            return View(users);
        }

        
        public async Task<ActionResult> UpdateChat(string id)
        {
            var model = await GetIndexFullAndPartial(id);

            return PartialView("PrivateChatWindowPartial", model);
        }

        #region
        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }



        #endregion
    }
}