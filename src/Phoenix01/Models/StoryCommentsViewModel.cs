using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class StoryCommentsViewModel
    {

        public Story story { get; set; }
        public List<Comment> comment { get; set; }
    }
}
