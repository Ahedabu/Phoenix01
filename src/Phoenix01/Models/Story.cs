using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class Story
    {
    
        public int ID { get; set; }
        public string Title { get; set; }
        public string StoryBody { get; set; }
        public string Category { get; set; }
        //public virtual ICollection<ApplicationUser> AspNetUsers { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
