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
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Rate 组件
    /// </summary>
    public sealed partial class Rate
    {
        private JSInterop<Rate>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 Rate DOM 元素实例
        /// </summary>
        private ElementReference RateElement { get; set; }

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        private string? ClassString => CssBuilder.Default("rate")
            .AddClass("disabled", IsDisable)
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string? GetItemClassString(int i) => CssBuilder.Default("rate-item")
            .AddClass("is-on", Value >= i)
            .Build();

        /// <summary>
        /// 获得/设置 组件值
        /// </summary>
        [Parameter]
        public int Value { get; set; }

        /// <summary>
        /// 获得/设置 是否禁用 默认为 false
        /// </summary>
        [Parameter]
        public bool IsDisable { get; set; }

        /// <summary>
        /// 获得/设置 组件值变化时回调委托
        /// </summary>
        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        /// <summary>
        /// 获得/设置 组件值变化时回调委托
        /// </summary>
        [Parameter]
        public Func<int, Task>? OnValueChanged { get; set; }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);
            if (firstRender)
            {
                if (Interop == null) Interop = new JSInterop<Rate>(JSRuntime);
                if (Interop != null) await Interop.Invoke(this, RateElement, "rate", nameof(Clicked));
            }
        }

        /// <summary>
        /// 文件上传成功后回调此方法
        /// </summary>
        [JSInvokable]
        public async Task Clicked(int val)
        {
            Value = val;
            if (ValueChanged.HasDelegate) await ValueChanged.InvokeAsync(Value);
            if (OnValueChanged != null) await OnValueChanged(Value);
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing) Interop?.Dispose();
        }
    }
}
