using Microsoft.AspNetCore.Mvc;
using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //return Content("Hello from the HomeController");

            var model = new Restaurant { Id = 1, Name = " My Pizza place" };

            //return new ObjectResult(model);

            return View(model);
        }
    }
}
