using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using api.DataAccess;

namespace api.Controllers
{
    public class TransactionController : Controller
    {
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            // var db = new Database();

            var result = new [] {
                new { EffectiveDate = "10/7/2017", Account = "Discover", Description = "Wedding", Category = "Personal", Amount = 250.00},
                new { EffectiveDate = "10/1/2017", Account = "RCU", Description = "Rent", Category = "Rent", Amount = 1600.00},
                new { EffectiveDate = "10/7/2017", Account = "Discover", Description = "Wedding", Category = "Personal", Amount = 250.00},
                new { EffectiveDate = "10/1/2017", Account = "RCU", Description = "Rent", Category = "Rent", Amount = 1600.00},
                new { EffectiveDate = "10/7/2017", Account = "Discover", Description = "Wedding", Category = "Personal", Amount = 250.00},
                new { EffectiveDate = "10/1/2017", Account = "RCU", Description = "Rent", Category = "Rent", Amount = 1600.00},
                new { EffectiveDate = "10/7/2017", Account = "Discover", Description = "Wedding", Category = "Personal", Amount = 250.00},
                new { EffectiveDate = "10/1/2017", Account = "RCU", Description = "Rent", Category = "Rent", Amount = 1600.00}
            };

            return Ok(result);
        }

        [HttpPost]
        [Route("api/[controller]/previewFileHeaderRow")]
        public IActionResult PreviewFileHeaderRow(string filePath) 
        {
            var errorMessage = String.Empty;

            try {
                using(var fileStream = new FileStream(filePath, FileMode.Open)) {
                    using (var streamReader =  new StreamReader(fileStream)) {
                        var preview = new Dictionary<int, List<string>>();

                        for (var lineNumber = 0; lineNumber < 5; lineNumber++) {
                            var line = streamReader.ReadLine();
                            if (line != null) {
                                preview.Add(lineNumber, new List<string>());
                                var columns = line.Split(',').ToList();
                                preview[lineNumber] = columns;
                            }
                        }

                        return Ok(preview);
                    }
                }
            } catch (IOException e) {
                Response.StatusCode = 400;
                errorMessage = String.Format("Error opening file at {0}", filePath);
            }

            return Json(new { status = "error", message = errorMessage});
        }

        [HttpPost]
        [Route("api/[controller]/uploadTransactions")]
        public IActionResult UploadTransactions(string filePath, bool hasHeaderRow, Dictionary<string, int> selectedCols) 
        {
            using(var fileStream = new FileStream(filePath, FileMode.Open)) {
                using (var streamReader =  new StreamReader(fileStream)) {
                    var line = streamReader.ReadLine();
                    if (line != null) {
                        
                    }
                }
            }

            return Ok(true);
        }
    }
}