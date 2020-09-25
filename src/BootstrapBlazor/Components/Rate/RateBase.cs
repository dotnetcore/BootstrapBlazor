using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

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
        /// 获得 样式集合
        /// </summary>
        protected virtual string? ClassString => CssBuilder.Default("rate")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

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
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                if (Interop == null) Interop = new JSInterop<RateBase>(JSRuntime);
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
