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
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Slider 组件
    /// </summary>
    public class SliderBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 JSInterop 实例
        /// </summary>
        protected JSInterop<SliderBase>? Interop { get; set; }

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected virtual string? ClassName => CssBuilder.Default("slider")
            .Build();

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected virtual string? SliderClassName => CssBuilder.Default("slider-runway")
            .AddClass("disabled", IsDisabled)
            .Build();

        /// <summary>
        /// 获得 Bar 位置样式
        /// </summary>
        protected virtual string? BarStyle => CssBuilder.Default("left: 0%;")
            .AddClass($"width: {Value}%;")
            .Build();

        /// <summary>
        /// 获得 按钮位置样式
        /// </summary>
        protected virtual string? ButtonStyle => CssBuilder.Default()
            .AddClass($"left: {Value}%;")
            .Build();

        /// <summary>
        /// 获得/设置 当前组件 DOM 实例
        /// </summary>
        protected ElementReference Slider { get; set; }

        /// <summary>
        /// 获得/设置 组件当前值
        /// </summary>
        [Parameter] public int Value { get; set; }

        /// <summary>
        /// ValueChanged 回调方法
        /// </summary>
        [Parameter] public EventCallback<int> ValueChanged { get; set; }

        /// <summary>
        /// 获得 按钮 disabled 属性
        /// </summary>
        protected string? Disabled => IsDisabled ? "true" : null;

        /// <summary>
        /// 获得/设置 是否禁用
        /// </summary>
        [Parameter] public bool IsDisabled { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                Interop = new JSInterop<SliderBase>(JSRuntime);
                await Interop.Invoke(this, Slider, "slider", nameof(SliderBase.SetValue));
            }
        }

        /// <summary>
        /// SetValue 方法
        /// </summary>
        /// <param name="val"></param>
        [JSInvokable]
        public void SetValue(int val)
        {
            Value = val;
            if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(val);
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing) Interop?.Dispose();
        }
    }
}
