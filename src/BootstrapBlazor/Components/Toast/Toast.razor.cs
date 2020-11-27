// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹出窗组件
    /// </summary>
    public partial class Toast
    {
        private string? ClassString => CssBuilder.Default("toast-container")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得 Toast 组件样式设置
        /// </summary>
        private string? StyleString => CssBuilder.Default()
            .AddClass("top: 1rem; left: 1rem;", Placement == Placement.TopStart)
            .AddClass("top: 1rem; right: 1rem;", Placement == Placement.TopEnd)
            .AddClass("bottom: 1rem; left: 1rem;", Placement == Placement.BottomStart)
            .AddClass("bottom: 1rem; right: 1rem;", Placement == Placement.BottomEnd)
            .Build();

        /// <summary>
        /// 获得 弹出窗集合
        /// </summary>
        private Dictionary<ToastOption, RenderFragment> Toasts { get; set; } = new Dictionary<ToastOption, RenderFragment>();

        private bool IsLeft() => Placement == Placement.TopStart || Placement == Placement.BottomStart;

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Placement = Placement.BottomEnd;

            // 注册 Toast 弹窗事件
            if (ToastService != null)
            {
                ToastService.Register(this, Show);
            }
        }

        private async Task Show(ToastOption option)
        {
            Toasts.Add(option, new RenderFragment(builder =>
            {
                var index = 0;
                builder.OpenComponent<ToastBox>(index++);
                builder.AddAttribute(index++, "class", IsLeft() ? "left" : "");
                builder.AddAttribute(index++, nameof(ToastBox.Category), option.Category);
                builder.AddAttribute(index++, nameof(ToastBox.Title), option.Title);
                builder.AddAttribute(index++, nameof(ToastBox.Content), option.Content);
                builder.AddAttribute(index++, nameof(ToastBox.IsAutoHide), option.IsAutoHide);
                builder.AddAttribute(index++, nameof(ToastBox.Delay), option.Delay);
                builder.CloseComponent();
            }));
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 清除 ToastBox 方法
        /// </summary>
        [JSInvokable]
        public async Task Clear()
        {
            Toasts.Clear();
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 设置 Toast 容器位置方法
        /// </summary>
        /// <param name="placement"></param>
        public void SetPlacement(Placement placement)
        {
            Placement = placement;
            StateHasChanged();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                ToastService.UnRegister(this);
            }
        }
    }
}
