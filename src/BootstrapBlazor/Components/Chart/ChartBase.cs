using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Chart 组件基类
    /// </summary>
    public abstract class ChartBase : BootstrapComponentBase
    {
        private JSInterop<ChartBase>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 EChart DOM 元素实例
        /// </summary>
        protected ElementReference Chart { get; set; }

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        protected string? ClassName => CssBuilder.Default("chart is-loading")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 组件 Style 字符串
        /// </summary>
        protected string? StyleString => CssBuilder.Default()
            .AddClass($"width: {Width};", !string.IsNullOrEmpty(Width))
            .Build();

        /// <summary>
        /// 获得/设置 组件宽度支持单位
        /// </summary>
        [Parameter]
        public string? Width { get; set; }

        /// <summary>
        /// 获得/设置 组件数据初始化委托方法
        /// </summary>
        [Parameter]
        public Func<Task<ChartDataSource>>? OnInit { get; set; }

        /// <summary>
        /// 获得/设置 客户端绘制图表完毕后回调此委托方法
        /// </summary>
        [Parameter]
        public Action? OnAfterInit { get; set; }

        /// <summary>
        /// 获得/设置 图表组件渲染类型 默认为 line 图
        /// </summary>
        [Parameter]
        public ChartType ChartType { get; set; } = ChartType.Line;

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            base.OnAfterRender(firstRender);

            if (firstRender)
            {
                if (OnInit == null) throw new InvalidOperationException("OnInit paramenter must be set");

                if (Interop == null && JSRuntime != null) Interop = new JSInterop<ChartBase>(JSRuntime);

                var ds = await OnInit.Invoke();

                Interop?.Invoke(this, Chart, "chart", nameof(Completed), ds);
            }
        }

        /// <summary>
        /// 图表绘制完成后回调此方法
        /// </summary>
        [JSInvokable]
        public void Completed()
        {
            OnAfterInit?.Invoke();
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
