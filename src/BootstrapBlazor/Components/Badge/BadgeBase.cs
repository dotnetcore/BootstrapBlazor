// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Badge 徽章组件
    /// </summary>
    public abstract class BadgeBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected string? ClassName => CssBuilder.Default("badge")
            .AddClass($"badge-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("badge-pill", IsPill)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        [Parameter] public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 是否显示为胶囊形式
        /// </summary>
        /// <value></value>
        [Parameter] public bool IsPill { get; set; }

        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter] public RenderFragment? ChildContent { get; set; }
    }
}
