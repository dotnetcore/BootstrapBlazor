using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

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
            .AddClass("show d-block", IsShow)
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
        /// 获得/设置 图标样式
        /// </summary>
        protected string? IconClass => CssBuilder.Default("fa")
            .AddClass(Icon)
            .Build();

        /// <summary>
        /// 获得/设置 组件样式信息
        /// </summary>
        protected string? StyleName => CssBuilder.Default()
            .AddClass($"left: -{MarginX:f2}px", MarginX.HasValue)
            .Build();

        /// <summary>
        /// 获得/设置 X 轴偏移量
        /// </summary>
        protected float? MarginX { get; set; }

        /// <summary>
        /// 获得/设置 确认弹框实例
        /// </summary>
        protected ElementReference ConfirmPopover { get; set; }

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
        /// 获得/设置 组件是否显示
        /// </summary>
        [Parameter] public bool IsShow { get; set; }

        /// <summary>
        /// 显示弹窗方法
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        public async Task Show(string title = "", string content = "")
        {
            Title = title;
            Content = content;
            StateHasChanged();

            // 计算位移量
            if (JSRuntime != null)
            {
                MarginX = await JSRuntime.Confirm(ConfirmPopover);
                IsShow = true;
                await InvokeAsync(StateHasChanged);
            }
        }

        /// <summary>
        /// 点击确认按钮回调方法
        /// </summary>
        protected void OnCloseClick()
        {
            IsShow = false;
        }

        /// <summary>
        /// 点击确认按钮回调方法
        /// </summary>
        protected void OnConfirmClick()
        {
            IsShow = false;
        }
    }
}
