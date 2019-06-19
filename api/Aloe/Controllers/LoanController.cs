using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aloe.ChartJs;
using Aloe.Domain.Loan;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Aloe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
		[HttpGet]
		public ActionResult<ChartJsData<string>> GetChartData()
		{
			var monthlyPayment = 105m;
			var loans = new List<LoanModel>()
			{
				new LoanModel()
				{
					Amount = 1500m,
					Rate = 0.04m,
					Name = "First Loan"
				},
				new LoanModel()
				{
					Amount = 2000m,
					Rate = 0.05m,
					Name = "Second Loan"
				}
			};

			var chartData = new LoanChartModel(loans, monthlyPayment);
			return chartData.ToChartObject();
		}
    }
}