using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using api.DataAccess.Tables;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Get()
        {
            var accountDataTable = new AccountTable();
            accountDataTable.Initialize();

            var accounts = accountDataTable.Select();      
            var result = accounts.Values.ToList();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create() 
        {
            return Ok(null);
        }
    }
}