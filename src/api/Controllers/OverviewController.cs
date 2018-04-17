using System;
using api.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class OverviewController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var query = new OverviewQueries();
            var overview = query.SelectOverview();

            return Ok(overview);
        }
    }
}