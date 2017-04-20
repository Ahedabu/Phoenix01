using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phoenix01.Models
{
    public class EditHobbiesViewModel
    {
        public EditHobbiesViewModel()
        {
            SelectedHobbies = new List<CheckBoxListItem>();
        }
        public string Id { get; set; }
        public List<CheckBoxListItem> SelectedHobbies { get; set; }
    }
}
