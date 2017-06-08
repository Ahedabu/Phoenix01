using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class StoryCommentsViewModel
    {


        public int Id { get; set; }

        public string Content { get; set; }

        public  DateTime CreatedDate { get; set; }

        public virtual Story Story { get; set; }

        public int StoryId { get; set; }

        public ApplicationUser LoggedInUser { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public List<Comment> comment { get; set; }
    }
}
