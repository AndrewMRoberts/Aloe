using System;
using Microsoft.AspNetCore.Mvc;

using api.DataAccess;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class TransactionController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var db = new Database();

            var result = new [] {
                new { FirstName = "Jimmy", LastName = "Bob"},
                new { FirstName = "Mike", LastName = "Krcynzski"}
            };

            return Ok(result);
        }
    }
}