using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using api.DataAccess;
using api.DataAccess.Tables;

namespace api.Controllers
{
    public class TransactionController : Controller
    {
        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult Get()
        {
            var transactionTable = new TransactionTable();
            var transactions = transactionTable.Select();

            var result = transactions.Values.ToList();

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
        public IActionResult UploadTransactions(string filePath, int account, bool hasHeaderRow, Dictionary<string, int> selectedCols) 
        {
            using(var fileStream = new FileStream(filePath, FileMode.Open)) {
                using (var streamReader =  new StreamReader(fileStream)) {
                    if (hasHeaderRow) {
                        streamReader.ReadLine();
                    }

                    var line = streamReader.ReadLine();
                    var transactionTable = new TransactionTable();
                    while (line != null) {
                        var columns = line.Split(',');
                        var transaction = new Transaction() 
                        {
                            Amount = Convert.ToDecimal(columns[selectedCols["amount"]]),
                            Description = columns[selectedCols["description"]],
                            TransactionDate = Convert.ToDateTime(columns[selectedCols["date"]]),
                            AccountId = account
                        };

                        transactionTable.Insert(transaction);

                        line = streamReader.ReadLine();
                    }
                }
            }

            return Ok(true);
        }
    }
}