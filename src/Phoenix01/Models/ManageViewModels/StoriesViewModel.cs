using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phoenix01.Models;
using Phoenix01.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;

namespace Phoenix01.Models.ManageViewModels
{
    public class StoriesViewModel
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string StoryBody { get; set; }
        public string Category { get; set; }
        //public virtual ICollection<ApplicationUser> AspNetUsers { get; set; }
        public string ApplicationUserId { get; set; }
        public List<Story> Stories { set; get; }
        public ApplicationUser StoryUser { get; set; }
        public ApplicationUser LoggedInUser { get; set; }
    }
}

