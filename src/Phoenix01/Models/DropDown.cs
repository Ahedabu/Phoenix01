using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class Country
    {
        public string ID { get; set; }
        public string Name { get; set; }
    }

    public class State
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string CountryId { get; set; }
        public Country Country { get; set; }
    }
    public class City
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string StateId { get; set; }
        public State State { get; set; }
    }

    public class DropViewModel
    {
        [Display(Name = "Country")]
        public string CountryName { get; set; }
        [Display(Name = "State")]
        public string StateName { get; set; }
        [Display(Name = "City")]
        public string CityName { get; set; }
        public SelectList Countries { get; set; }
        public SelectList States { get; set; }
        public SelectList Cities { get; set; }
    }
}
