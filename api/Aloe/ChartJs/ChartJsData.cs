using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aloe.ChartJs
{
	public class ChartJsData<T>
	{
		public IEnumerable<ChartJsDataset<T>> datasets { get; set; }
		public IEnumerable<string> labels { get; set; }

		public ChartJsData()
		{
			datasets = new List<ChartJsDataset<T>>();
			labels = new List<string>();
		}
	}
}
