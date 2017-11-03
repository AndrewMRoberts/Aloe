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
            // var db = new Database();

            var result = new [] {
                new { EffectiveDate = "10/7/2017", Account = "Discover", Description = "Wedding", Category = "Personal", Amount = 250.00},
                new { EffectiveDate = "10/1/2017", Account = "RCU", Description = "Rent", Category = "Rent", Amount = 1600.00}
            };

            return Ok(result);
        }
    }
}