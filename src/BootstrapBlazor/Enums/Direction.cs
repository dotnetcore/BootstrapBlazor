using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor
{
    /// <summary>
    /// 下拉框枚举类
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// 
        /// </summary>
        None,

        /// <summary>
        /// Dropup
        /// </summary>
        [Description("dropup")]
        Dropup,

        /// <summary>
        /// Dropleft
        /// </summary>
        [Description("dropleft")]
        Dropleft,

        /// <summary>
        /// Dropright
        /// </summary>
        [Description("dropright")]
        Dropright
    }
}
