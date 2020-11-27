// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ITooltip 接口
    /// </summary>
    public interface ITooltip
    {
        /// <summary>
        /// 获得/设置 位置
        /// </summary>
        Placement Placement { get; set; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        string? Title { get; set; }

        /// <summary>
        /// 获得/设置 显示内容
        /// </summary>
        string? Content { get; set; }

        /// <summary>
        /// 获得/设置 内容是否为 Html
        /// </summary>
        bool IsHtml { get; set; }

        /// <summary>
        /// 获得/设置 弹出方式 默认为 Tooltip
        /// </summary>
        PopoverType PopoverType { get; set; }

        /// <summary>
        /// 获得/设置 触发方式 可组合 click focus hover 默认为 focus hover
        /// </summary>
        string Trigger { get; set; }
    }
}
