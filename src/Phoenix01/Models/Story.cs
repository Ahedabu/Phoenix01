using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class Story :BaseEntity
    {

        public Story()
        {
            this.Comments = new HashSet<Comment>();
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public string StoryBody { get; set; }
        public string Category { get; set; }
          
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime CreatedDate { get; set; }

        public IEnumerable<Comment> Comments { get; set; }

        

    }
}
