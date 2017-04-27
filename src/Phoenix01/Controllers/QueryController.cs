using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Phoenix01.Controllers
{
    public class QueryController: Controller
    {
        // GET: Stories
        public IActionResult jQueryTest()
        {
            return View();
        }

    }
}
