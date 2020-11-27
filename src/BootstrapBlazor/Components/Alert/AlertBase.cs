// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Alert 警告框组件
    /// </summary>
    public abstract class AlertBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected virtual string? ClassName => CssBuilder.Default("alert fade show")
            .AddClass($"alert-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("is-bar", ShowBar)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 颜色
        /// </summary>
        [Parameter]
        public Color Color { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 是否显示关闭按钮
        /// </summary>
        [Parameter]
        public bool ShowDismiss { get; set; }

        /// <summary>
        /// 获得/设置 显示图标
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 是否显示左侧 Bar
        /// </summary>
        [Parameter]
        public bool ShowBar { get; set; }

        /// <summary>
        /// 子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 关闭警告框回调方法
        /// </summary>
        [Parameter]
        public EventCallback<MouseEventArgs> OnDismiss { get; set; }
    }
}
