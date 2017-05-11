using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Phoenix01.Models
{
    public class Comment : BaseEntity 
    {
       
       
        public string comment { get; set; }

        public int id { get; set; }

        public DateTime CreatedDate { get; set; }

        public virtual Story story { get; set; }

        public string ApplicationUserId { get; set; }

        public int ParentId { get; set; }

        public ApplicationUser applicationUser { get; set; }
    }
}
