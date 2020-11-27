// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Drawer 组件基类
    /// </summary>
    public abstract class DrawerBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("drawer-wrapper")
            .AddClass("is-open", IsOpen)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 抽屉 Style 字符串
        /// </summary>
        protected string? DrawerStyleString => CssBuilder.Default()
            .AddClass($"width: {Width};", !string.IsNullOrEmpty(Width) && Placement != Placement.Top && Placement != Placement.Bottom)
            .AddClass($"height: {Height};", !string.IsNullOrEmpty(Height) && (Placement == Placement.Top || Placement == Placement.Bottom))
            .Build();

        /// <summary>
        /// 获得 抽屉样式
        /// </summary>
        protected string? DrawerClassString => CssBuilder.Default("drawer")
            .AddClass("left", Placement != Placement.Right && Placement != Placement.Top && Placement != Placement.Bottom)
            .AddClass("top", Placement == Placement.Top)
            .AddClass("right", Placement == Placement.Right)
            .AddClass("bottom", Placement == Placement.Bottom)
            .Build();

        /// <summary>
        /// 获得/设置 抽屉宽度 左右布局时生效
        /// </summary>
        [Parameter]
        public string Width { get; set; } = "360px";

        /// <summary>
        /// 获得/设置 抽屉高度 上下布局时生效
        /// </summary>
        [Parameter]
        public string Height { get; set; } = "290px";

        /// <summary>
        /// 获得/设置 抽屉是否打开 默认 false 未打开
        /// </summary>
        [Parameter]
        public bool IsOpen { get; set; }

        /// <summary>
        /// 获得/设置 IsOpen 属性改变时回调委托方法
        /// </summary>
        [Parameter]
        public EventCallback<bool> IsOpenChanged { get; set; }

        /// <summary>
        /// 获得/设置 点击背景遮罩时回调委托方法
        /// </summary>
        [Parameter]
        public Action? OnClickBackdrop { get; set; }

        /// <summary>
        /// 获得/设置 点击遮罩是否关闭抽屉
        /// </summary>
        [Parameter]
        public bool IsBackdrop { get; set; }

        /// <summary>
        /// 获得/设置 组件出现位置 默认显示在 Left 位置
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; } = Placement.Left;

        /// <summary>
        /// 获得/设置 子组件
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 点击背景遮罩方法
        /// </summary>
        public void OnContainerClick()
        {
            if (IsBackdrop)
            {
                IsOpen = false;
                if (IsOpenChanged.HasDelegate) IsOpenChanged.InvokeAsync(IsOpen);
                OnClickBackdrop?.Invoke();
            }
        }
    }
}
