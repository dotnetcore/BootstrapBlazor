using BootstrapBlazor.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.WebConsole.Pages
{
    /// <summary>
    /// 
    /// </summary>
    sealed partial class ECharts
    {
        private static Random Randomer { get; set; } = new Random();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private async Task<EChartDataSource> OnInit()
        {
            var ds = new EChartDataSource();
            ds.Name = "测试数据";
            ds.Legend.Add("测试数据");

            var data = await GenerateData();
            ds.Data.AddRange(data);
            return ds;
        }

        private Task<IEnumerable<KeyValuePair<string, object>>> GenerateData()
        {
            return Task.FromResult(Enumerable.Range(1, 60).Select(i => new KeyValuePair<string, object>(DateTime.Today.AddDays(i).ToString("yyyy-MM-dd"), Convert.ToInt32(Randomer.Next(0, 10) / 1.0))));
        }
    }
}
