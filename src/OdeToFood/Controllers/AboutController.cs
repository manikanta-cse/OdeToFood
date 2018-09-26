using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.Controllers
{
    //[Route("about")]
    [Route("company/[controller]/[action]")] // token based, more robust
    public class AboutController
    {
        //[Route("")]
        public string Phone()
        {
            return "+91-66564564546546";
        }

        //[Route("address")]
        //[Route("[action]")]
        public string Address()
        {
            return "India";
        }
    }
}
