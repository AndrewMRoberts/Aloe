using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloe.Domain.Loan
{
	public class LoanModel
	{
		public int Id { get; set; }
		public decimal Amount { get; set; }
		public decimal Rate { get; set; }
		public string Name { get; set; }
	}
}
