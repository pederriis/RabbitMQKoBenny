using System;
using Microsoft.AspNetCore.Mvc;

namespace LoginService.Controllers
{
    public class TestController : Controller
    {
        private Random r;

        public TestController()
        {
            r = new Random();
        }


        [HttpGet("/test/")]
        public IActionResult Test()
        {
            return Ok("Test controller: " + r.Next(0 , 1000).ToString());
        }
    }
}
