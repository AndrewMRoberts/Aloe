using System;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class OverviewController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = new [] {
                new { FirstName = "Jimmy", LastName = "Bob"},
                new { FirstName = "Mike", LastName = "Krcynzski"},
                new { FirstName = "Mowry", LastName = "Love"}
            };

            return Ok(result);
        }
    }
}