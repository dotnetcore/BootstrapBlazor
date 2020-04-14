using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Popover Confirm 组件
    /// </summary>
    public abstract class PopoverConfirmBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassName => CssBuilder.Default("popover-confirm fade")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 关闭按钮样式
        /// </summary>
        protected string? CloseButtonClass => CssBuilder.Default("btn btn-xs")
            .AddClass($"btn-{CloseButtonColor.ToDescriptionString()}")
            .Build();

        /// <summary>
        /// 获得 关闭按钮样式
        /// </summary>
        protected string? ConfirmButtonClass => CssBuilder.Default("btn btn-xs")
            .AddClass($"btn-{ConfirmButtonColor.ToDescriptionString()}")
            .Build();

        /// <summary>
        /// 获得 图标样式
        /// </summary>
        protected string? IconClass => CssBuilder.Default("fa")
            .AddClass(Icon)
            .Build();

        /// <summary>
        /// 获得 确认框位置信息
        /// </summary>
        protected string? PlacementString => CssBuilder.Default()
            .AddClass("bottom", Placement == Placement.Bottom)
            .AddClass("top", Placement != Placement.Bottom)
            .Build();

        /// <summary>
        /// 获得/设置 确认弹框实例
        /// </summary>
        protected ElementReference ConfirmPopover { get; set; }

        /// <summary>
        /// 获得/设置 客户端执行命令
        /// </summary>
        protected string? MethodString { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        [Inject] protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 获得/设置 显示标题
        /// </summary>
        [Parameter] public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        [Parameter] public string Content { get; set; } = "Popover Confirm";

        /// <summary>
        /// 获得/设置 确认框弹出位置 默认为 top 上方
        /// </summary>
        [Parameter] public Placement Placement { get; set; } = Placement.Top;

        /// <summary>
        /// 获得/设置 关闭按钮显示文字
        /// </summary>
        [Parameter] public string CloseButtonText { get; set; } = "关闭";

        /// <summary>
        /// 获得/设置 确认按钮颜色
        /// </summary>
        [Parameter] public Color CloseButtonColor { get; set; } = Color.Secondary;

        /// <summary>
        /// 获得/设置 确认按钮显示文字
        /// </summary>
        [Parameter] public string ConfirmButtonText { get; set; } = "确定";

        /// <summary>
        /// 获得/设置 确认按钮颜色
        /// </summary>
        [Parameter] public Color ConfirmButtonColor { get; set; } = Color.Primary;

        /// <summary>
        /// 获得/设置 确认框图标
        /// </summary>
        [Parameter] public string? Icon { get; set; } = "fa-exclamation-circle text-info";

        /// <summary>
        /// 点击确认按钮回调方法
        /// </summary>
        [Parameter] public Action? OnConfirm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (!string.IsNullOrEmpty(MethodString)) JSRuntime.Confirm(ConfirmPopover, MethodString);
        }

        /// <summary>
        /// 显示弹窗方法
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public void Show(string title = "", string content = "")
        {
            Title = title;
            Content = content;
            MethodString = "show";
            StateHasChanged();
        }

        /// <summary>
        /// 点击确认按钮回调方法
        /// </summary>
        protected void OnCloseClick()
        {
            MethodString = "close";
        }

        /// <summary>
        /// 点击确认按钮回调方法
        /// </summary>
        protected void OnConfirmClick()
        {
            MethodString = "close";
            OnConfirm?.Invoke();
        }
    }
}
