using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phoenix01.Models;
using Phoenix01.Data;

namespace Phoenix01.Controllers
{
    public class DropDownController : Controller
    {

        ApplicationDbContext _db;
        public DropDownController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Home()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Submit(DropViewModel model)
        {
            return View(model);
        }

        public PartialViewResult Country(DropViewModel model)
        {
            SelectList countries = GetCountries();
            model.Countries = countries;
            return PartialView(model);
        }

        [HttpPost]
        public PartialViewResult State(DropViewModel model)
        {
            string countryId = _db.Countries.Where(x => x.Name == model.CountryName).Select(x => x.ID).FirstOrDefault();
            SelectList states = GetCountryStates(countryId);
            model.States = states;
            return PartialView(model);
        }

        [HttpPost]
        public PartialViewResult City(DropViewModel model)
        {
            string stateId = _db.States.Where(x => x.Name == model.StateName).Select(x => x.ID).FirstOrDefault();
            SelectList cities = GetStateCities(stateId);
            model.Cities = cities;
            return PartialView(model);
        }

        public SelectList GetCountries()
        {
            var countries = _db.Countries.ToList();
            SelectList nameAdded = new SelectList(countries, "Name", "Name");
            return nameAdded;
        }

        public SelectList GetCountryStates(string id)
        {
            var states = _db.States.Where(x => x.CountryId == id).ToList();
            SelectList nameAdded = new SelectList(states, "Name", "Name");
            return nameAdded;
        }

        public SelectList GetStateCities(string id)
        {
            var cities = _db.Cities.Where(x => x.StateId == id).ToList();
            SelectList nameAdded = new SelectList(cities, "Name", "Name");
            return nameAdded;
        }
    }
}