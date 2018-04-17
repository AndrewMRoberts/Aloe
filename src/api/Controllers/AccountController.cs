using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using api.DataAccess.Tables;

namespace api.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        [Route("api/[controller]")]
        [Route("api/[controller]/Get")]
        public IActionResult Get()
        {
            var accountDataTable = new AccountTable();

            var accounts = accountDataTable.Select();      
            var result = accounts.Values.ToList();

            return Ok(result);
        }

        [HttpPost]
        [Route("api/[controller]/create")]
        public IActionResult Create(string name, bool isCredit) 
        {
            var table = new AccountTable();
            table.Insert(new DataAccess.Account() {Name = name, IsCredit = isCredit});
            return Ok(null);
        }

        [HttpPost]
        [Route("api/[controller]/remove")]
        public IActionResult Remove(int id) 
        {
            var table = new AccountTable();
            table.Remove(id);

            return Ok(null);
        }
    }
}