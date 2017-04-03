﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Phoenix01.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Phoenix01.Data;

namespace Phoenix01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()//wellcome
        {
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //var currentUser = manager.FindById(User.Identity.GetUserId());
            //ViewBag.email = currentUser.UserProfileInfo.email;





            return View();
        }

        //home page layout.
        public IActionResult Story()
        {
            ViewData["Message"] = "................Stories...............";
            return View();
        }



        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
