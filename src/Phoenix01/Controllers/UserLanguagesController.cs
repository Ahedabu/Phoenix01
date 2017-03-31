using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace Phoenix01.Controllers
{
    public class UserLanguagesController : Controller
    {
        // GET: UserLanguages
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserLanguages/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserLanguages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserLanguages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserLanguages/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserLanguages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserLanguages/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserLanguages/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}