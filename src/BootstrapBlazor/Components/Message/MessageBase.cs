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
    /// Message 组件基类
    /// </summary>
    public abstract class MessageBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得 组件样式
        /// </summary>
        protected string? ClassString => CssBuilder.Default("message")
            .AddClass("is-bottom", Placement != Placement.Top)
            .Build();

        /// <summary>
        /// 获得 Toast 组件样式设置
        /// </summary>
        protected string? StyleName => CssBuilder.Default()
            .AddClass("top: 1rem;", Placement != Placement.Bottom)
            .AddClass("bottom: 1rem;", Placement == Placement.Bottom)
            .Build();

        /// <summary>
        /// 获得 弹出窗集合
        /// </summary>
        private List<MessageOption> _messages { get; set; } = new List<MessageOption>();

        /// <summary>
        /// 获得 弹出窗集合
        /// </summary>
        protected IEnumerable<MessageOption> Messages => _messages;

        /// <summary>
        /// 获得/设置 显示位置 默认为 Top
        /// </summary>
        [Parameter]
        public Placement Placement { get; set; } = Placement.Top;

        /// <summary>
        /// ToastServices 服务实例
        /// </summary>
        [Inject]
        public MessageService? MessageService { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            // 注册 Toast 弹窗事件
            MessageService?.Register(this, Show);
        }

        private async Task Show(MessageOption option)
        {
            _messages.Add(option);
            await InvokeAsync(StateHasChanged);
        }

        /// <summary>
        /// 清除 ToastBox 方法
        /// </summary>
        [JSInvokable]
        public void Clear()
        {
            _messages.Clear();
            InvokeAsync(StateHasChanged).ConfigureAwait(false);
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
                MessageService?.UnRegister(this);
            }
        }
    }
}
