using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;



namespace Phoenix01.Models.ManageViewModels
{
    public class StoriesInfoViewModel
    {


        public int ID { get; set; }

        public string Title { get; set; }

        public string StoryBody { get; set; }

        public string UserName { get; set; }

        [Display(Name = "UserImage")]
        public string UserImage { get; set; }


        
        public ApplicationUser applicationUser { get; set; }
    
        public ICollection<Story> stories { get; set; }

    }
}
