using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Server.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchModel
    {
        /// <summary>
        /// 
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? SubTitle { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<DemoBlock>? DemoBlocks { get; set; } = new List<DemoBlock>();
    }
}
