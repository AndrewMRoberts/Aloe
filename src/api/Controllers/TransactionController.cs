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
                new { EffectiveDate = "10/7/2017", Description = "Wedding", Category = "Personal", Amount = 250.00}
            };

            return Ok(result);
        }
    }
}