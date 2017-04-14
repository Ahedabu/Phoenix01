using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class Hobby
    {
      
        public int HobbyId { get; set; }
        public string HobbyName { get; set; }
        [NotMapped]
        public bool CheckboxAnswer { get; set; }
        public List<ApplicationUserHobby> ApplicationUserHobby { get; set; }
    }
}
