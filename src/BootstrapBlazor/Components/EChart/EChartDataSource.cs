using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// EChart 图表组件数据源
    /// </summary>
    public class EChartDataSource
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> Legend { get; set; } = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public List<KeyValuePair<string, object>> Data { get; set; } = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; } = "未设置";
    }
}
