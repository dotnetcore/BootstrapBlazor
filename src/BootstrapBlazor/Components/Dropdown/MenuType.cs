using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 下拉框类型
    /// </summary>
    public enum MenuType
    {
        /// <summary>
        /// 
        /// </summary>
        _,

        /// <summary>
        /// 
        /// </summary>
        [Description("dropdown")]
        Dropmenu,

        /// <summary>
        /// 
        /// </summary>
        [Description("btn-group")]
        Btngroup
    }
}
