using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Rate 组件基类
    /// </summary>
    public abstract class RateBase : BootstrapComponentBase
    {
        private JSInterop<RateBase>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 Rate DOM 元素实例
        /// </summary>
        protected ElementReference RateElement { get; set; }

        /// <summary>
        /// 获得/设置 组件值
        /// </summary>
        [Parameter]
        public int Value { get; set; }

        /// <summary>
        /// 获得/设置 组件值变化时回调委托
        /// </summary>
        [Parameter]
        public EventCallback<int> ValueChanged { get; set; }

        /// <summary>
        /// 获得/设置 组件值变化时回调委托
        /// </summary>
        [Parameter]
        public Action<int>? OnValueChanged { get; set; }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if (Interop == null && JSRuntime != null) Interop = new JSInterop<RateBase>(JSRuntime);
                Interop?.Invoke(this, RateElement, "rate", nameof(Clicked));
            }
        }

        /// <summary>
        /// 文件上传成功后回调此方法
        /// </summary>
        [JSInvokable]
        public void Clicked(int val)
        {
            Value = val;
            if (ValueChanged.HasDelegate) ValueChanged.InvokeAsync(Value);
            OnValueChanged?.Invoke(Value);
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
