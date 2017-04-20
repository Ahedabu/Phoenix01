using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;



namespace Phoenix01.Models.AccountViewModels
{
    public class EditProfile
    {


        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int AreaCode { get; set; }
        [Required]
        public string StreetName { get; set; }
        [Required]
        public string Area { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        

        public string Image { get; set; }


        public DateTime RegistrationDate { get; set; }





    }
}
