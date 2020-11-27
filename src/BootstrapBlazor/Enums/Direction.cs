// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using System.ComponentModel;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 下拉框枚举类
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("dropdown")]
        Dropdown,

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
