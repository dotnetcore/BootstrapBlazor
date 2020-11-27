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
    /// Table 视图枚举类型
    /// </summary>
    public enum TableRenderModel
    {
        /// <summary>
        /// 自动
        /// </summary>
        [Description("自动")]
        Auto,

        /// <summary>
        /// Table 布局适用于大屏幕
        /// </summary>
        [Description("表格布局")]
        Table,

        /// <summary>
        /// 卡片式布局适用于小屏幕
        /// </summary>
        [Description("卡片布局")]
        CardView
    }
}
