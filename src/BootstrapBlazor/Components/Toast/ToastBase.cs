using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Toast 弹出窗组件
    /// </summary>
    public abstract class ToastBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 Toast 组件样式设置
        /// </summary>
        protected string? StyleName => CssBuilder.Default("position: fixed; z-index: 1040;")
            .AddClass("top: 1rem; right: 1rem;", Placement != Placement.BottomEnd)
            .AddClass("bottom: 1rem; right: 1rem;", Placement == Placement.BottomEnd)
            .Build();

        /// <summary>
        /// 获得 弹出窗集合
        /// </summary>
        protected Dictionary<ToastOption, RenderFragment> Toasts { get; set; } = new Dictionary<ToastOption, RenderFragment>();

        /// <summary>
        /// ToastServices 服务实例
        /// </summary>
        [Inject] public ToastService? ToastService { get; set; }

        /// <summary>
        /// 获得/设置 显示文字
        /// </summary>
        [Parameter] public Placement Placement { get; set; } = Placement.BottomEnd;

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 注册 Toast 弹窗事件
            if (ToastService != null)
            {
                ToastService.Register(Show);
            }
        }

        private async Task Show(ToastOption option)
        {
            Toasts.Add(option, new RenderFragment(builder =>
            {
                var index = 0;
                builder.OpenComponent<ToastBox>(index++);
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
    }
}
