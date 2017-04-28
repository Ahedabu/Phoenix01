using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Phoenix01.Models
{
    public class AjaxViewModel
    {
        public string Width { set; get; }
        public string Height { set; get; }

        public List<SelectListItem> Widgets { set; get; }

        public int? SelectedWidget { set; get; }
    }
}

