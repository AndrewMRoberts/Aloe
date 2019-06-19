using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloe.ChartJs
{
	public class ChartJsDataPoint<T>
	{
		public T x { get; set; }
		public decimal y { get; set; }

		public ChartJsDataPoint(T xAxis, decimal yAxis)
		{
			x = xAxis;
			y = yAxis;
		}
	}
}
