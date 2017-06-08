using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Phoenix01.Models
{
    public class Comment 
    {
       
        public string Content { get; set; }

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Story Story { get; set; }

        public int StoryId { get; set; }
        
        public ApplicationUser ApplicationUser { get; set; }
    }
}
