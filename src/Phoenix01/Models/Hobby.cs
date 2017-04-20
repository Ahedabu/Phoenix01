﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class Hobby
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public List<ApplicationUserHobby> ApplicationUserHobby { get; set; }
    }
}
