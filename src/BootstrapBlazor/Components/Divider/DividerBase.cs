// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Divider 组件基类
    /// </summary>
    public abstract class DividerBase : ComponentBase
    {
        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected virtual string? ClassString => CssBuilder.Default("divider")
            .AddClass("divider-horizontal", !IsVertical)
            .AddClass("divider-vertical", IsVertical)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 class 样式集合
        /// </summary>
        protected virtual string? TextClassString => CssBuilder.Default("divider-text")
            .AddClass("is-left", Alignment.Left == Alignment)
            .AddClass("is-center", Alignment.Center == Alignment)
            .AddClass("is-right", Alignment.Right == Alignment)
            .Build();

        /// <summary>
        /// 获得/设置 用户自定义属性
        /// </summary>
        /// <returns></returns>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 获得/设置 是否为垂直显示 默认为 false 
        /// </summary>
        [Parameter]
        public bool IsVertical { get; set; }

        /// <summary>
        /// 获得/设置 组件对齐方式 默认为居中
        /// </summary>
        [Parameter]
        public Alignment Alignment { get; set; } = Alignment.Center;

        /// <summary>
        /// 获得/设置 文案显示文字
        /// </summary>
        [Parameter]
        public string? Text { get; set; }

        /// <summary>
        /// 获得/设置 文案显示图标
        /// </summary>
        [Parameter]
        public string? Icon { get; set; }

        /// <summary>
        /// 获得/设置 子内容
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }
    }
}
