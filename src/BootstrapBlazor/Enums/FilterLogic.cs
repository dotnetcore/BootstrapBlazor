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
    /// 逻辑运算符
    /// </summary>
    public enum FilterLogic
    {
        /// <summary>
        /// 并且
        /// </summary>
        [Description("并且")]
        And,

        /// <summary>
        /// 或者
        /// </summary>
        [Description("或者")]
        Or
    }
}
