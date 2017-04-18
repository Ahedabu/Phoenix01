using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Phoenix01.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Phoenix01.Data;
using Microsoft.AspNetCore.Hosting;

namespace Phoenix01.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()//wellcome
        {
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
