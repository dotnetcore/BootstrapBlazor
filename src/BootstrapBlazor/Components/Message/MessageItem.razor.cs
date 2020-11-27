// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// MessageItem 组件
    /// </summary>
    public sealed partial class MessageItem
    {
        private ElementReference MessageItemElement { get; set; }

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected override string? ClassName => CssBuilder.Default("alert")
            .AddClass($"alert-{Color.ToDescriptionString()}", Color != Color.None)
            .AddClass("is-bar", ShowBar)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 Toast Body 子组件
        /// </summary>
        [Parameter]
        public string? Content { get; set; }

        /// <summary>
        /// 获得/设置 是否自动隐藏
        /// </summary>
        [Parameter]
        public bool IsAutoHide { get; set; } = true;

        /// <summary>
        /// 获得/设置 自动隐藏时间间隔
        /// </summary>
        [Parameter]
        public int Delay { get; set; } = 4000;

        /// <summary>
        /// 获得/设置 Message 实例
        /// </summary>
        /// <value></value>
        [CascadingParameter] public MessageBase? Message { get; set; }

        private JSInterop<MessageBase>? _interop;

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && Message != null)
            {
                _interop = new JSInterop<MessageBase>(JSRuntime);
                await _interop.Invoke(Message, MessageItemElement, "showMessage", nameof(MessageBase.Clear));
            }
        }
    }
}
