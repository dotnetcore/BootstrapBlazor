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
    /// ITooltipHost 接口 
    /// </summary>
    public interface ITooltipHost
    {
        /// <summary>
        /// 获得/设置 ITooltip 实例
        /// </summary>
        ITooltip? Tooltip { get; set; }
    }
}
