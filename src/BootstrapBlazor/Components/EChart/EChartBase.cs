using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// EChart 组件基类
    /// </summary>
    public abstract class EChartBase : BootstrapComponentBase
    {
        private JSInterop<EChartBase>? Interop { get; set; }

        /// <summary>
        /// 获得/设置 EChart DOM 元素实例
        /// </summary>
        protected ElementReference EChart { get; set; }

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        protected string? ClassName => CssBuilder.Default("echart")
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        /// <summary>
        /// 获得/设置 组件 Style 字符串
        /// </summary>
        protected string? StyleString => CssBuilder.Default()
            .AddClass($"height: {Height}px;", Height > 0)
            .Build();

        /// <summary>
        /// 获得/设置 组件高度
        /// </summary>
        [Parameter]
        public int Height { get; set; }

        /// <summary>
        /// 获得/设置 组件数据初始化委托方法
        /// </summary>
        [Parameter]
        public Func<Task<EChartDataSource>>? OnInit { get; set; }

        /// <summary>
        /// 获得/设置 客户端绘制图表完毕后回调此委托方法
        /// </summary>
        [Parameter]
        public Action? OnAfterInit { get; set; }

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

                if (Interop == null && JSRuntime != null) Interop = new JSInterop<EChartBase>(JSRuntime);

                var ds = await OnInit.Invoke();

                Interop?.Invoke(this, EChart, "echart", nameof(Completed), ds);
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
