using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// IToolbarButton 接口
    /// </summary>
    public interface IToolbarButton<TItem>
    {
        /// <summary>
        /// 
        /// </summary>
        Func<IEnumerable<TItem>, Task>? OnClickCallback { get; set; }
    }
}
