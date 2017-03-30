using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace Phoenix01.Models.ManageViewModels
{
    public class ForPictureViewModel
    {

        public IndexViewModel indexViewModel { get;  set; }
        public ApplicationUser applicationUser { get; set; }
    }
}
