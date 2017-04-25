using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Phoenix01.Models
{
    public class AjaxViewModel
    {
        public int Width { set; get; }
        public int Height { set; get; }

        public List<SelectListItem> Widgets { set; get; }

        public int? SelectedWidget { set; get; }
    }
}

