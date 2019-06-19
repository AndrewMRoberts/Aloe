using Aloe.ChartJs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloe.Domain.Loan
{
	public class LoanChartModel
	{
		public IList<LoanModel> Loans { get; set; }
		public decimal MonthlyPayment { get; set; }

		public LoanChartModel(IEnumerable<LoanModel> loans, decimal monthlyPayment)
		{
			Loans = loans.OrderByDescending(l => l.Rate).ToList();
			MonthlyPayment = monthlyPayment;

			var highestInterestLoan = Loans.FirstOrDefault();
			if (highestInterestLoan.Amount * highestInterestLoan.Rate >= MonthlyPayment)
			{
				throw new InvalidOperationException("The supplied monthly payment will never terminate the loan.");
			}
		}

		public ChartJsData<string> ToChartObject()
		{
			var data = new ChartJsData<string>();
			var datasets = new List<ChartJsDataset<string>>();

			var leftoverPayment = 0m;
			DateTime datePointer = DateTime.Today;
			foreach (var loan in Loans)
			{
				var dataset = new ChartJsDataset<string>()
				{
					label = loan.Name
				};

				// Handle all previous date periods for the chart to stack lines correctly
				var allPreviousPaymentDates = datasets.SelectMany(d => d.data).Select(d => d.x).Distinct().ToList();
				if (leftoverPayment > 0)
				{
					// Remove the current date pointer if there is a leftover, going to make a payment on this date in
					// the normal amortization schedule already.
					allPreviousPaymentDates.Remove(datePointer.ToString("MM/yy"));
				}

				foreach (var paymentDate in allPreviousPaymentDates)
				{
					dataset.data.Add(new ChartJsDataPoint<string>(paymentDate, loan.Amount));
				}

				// Calculate the new amortization schedule for the loan
				var beginningLoanAmount = loan.Amount - leftoverPayment;
				dataset.data.Add(new ChartJsDataPoint<string>(datePointer.ToString("MM/yy"), beginningLoanAmount));

				var amountLeftOnLoan = beginningLoanAmount;
				while (amountLeftOnLoan - MonthlyPayment > 0)
				{
					amountLeftOnLoan -= MonthlyPayment;
					datePointer = datePointer.AddMonths(1);
					dataset.data.Add(new ChartJsDataPoint<string>(datePointer.ToString("MM/yy"), amountLeftOnLoan));
				}

				leftoverPayment = MonthlyPayment - amountLeftOnLoan;
				amountLeftOnLoan = 0;
				datePointer = datePointer.AddMonths(1);
				dataset.data.Add(new ChartJsDataPoint<string>(datePointer.ToString("MM/yy"), amountLeftOnLoan));

				datasets.Add(dataset);
			}

			var labels = datasets.SelectMany(d => d.data).Select(d => d.x).Distinct();

			data.datasets = datasets;
			data.labels = labels;

			return data;
		}
	}
}
