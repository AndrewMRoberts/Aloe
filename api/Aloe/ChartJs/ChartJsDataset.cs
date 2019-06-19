using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloe.ChartJs
{
	public class ChartJsDataset<T>
	{
		public string label { get; set; }
		public IList<ChartJsDataPoint<T>> data { get; set; }

		public ChartJsDataset()
		{
			data = new List<ChartJsDataPoint<T>>();
		}
	}
}
