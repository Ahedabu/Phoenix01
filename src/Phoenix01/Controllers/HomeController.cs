﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Phoenix01.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Phoenix01.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Phoenix01.Controllers
{
    public class HomeController : Controller
    {

        private IHostingEnvironment hostingEnv;

        public HomeController(IHostingEnvironment env)
        {
            this.hostingEnv = env;
        }


        public IActionResult Index()
        {
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
