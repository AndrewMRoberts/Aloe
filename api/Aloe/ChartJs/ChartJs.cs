using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloe.ChartJs
{
	public class ChartJs<T>
	{
		public Enum.ChartType Type { get; set; }
		public ChartJsData<T> Data { get; set; }
		public ChartJsOptions Options { get; set; }
	}
}
