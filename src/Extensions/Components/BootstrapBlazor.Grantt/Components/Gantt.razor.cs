using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Gantt
    {
        /// <summary>
        ///  获得或设置 甘特图数据源
        /// </summary>
        [Parameter]
#if NET6_0_OR_GREATER
        [EditorRequired]
#endif
        public IEnumerable<GanttItem>? Items { get; set; }

        /// <summary>
        /// 获得或设置 甘特图配置项
        /// </summary>
        [Parameter]
        public GanttOption Option { get; set; } = new GanttOption();

        /// <summary>
        /// 获得或设置 点击事件
        /// </summary>
        [Parameter]
        public Func<GanttItem, Task>? OnClick { get; set; }

        /// <summary>
        /// 获得或设置 任务时间改变事件
        /// </summary>
        [Parameter]
        public Func<GanttItem, string, string, Task>? OnDataChanged { get; set; }

        /// <summary>
        /// 获得或设置 任务进度改变事件
        /// </summary>
        [Parameter]
        public Func<GanttItem, int, Task>? OnProgressChanged { get; set; }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await InvokeVoidAsync("init", Id, Items, Option, Interop);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task OnGanttClick(GanttItem item)
        {
            if (OnClick != null)
            {
                await OnClick(item);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="progress"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task OnGanttProgressChange(GanttItem item, int progress)
        {
            if (OnProgressChanged != null)
            {
                await OnProgressChanged(item, progress);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        [JSInvokable]
        public async Task OnGanttDataChange(GanttItem item, string start, string end)
        {
            if (OnDataChanged != null)
            {
                await OnDataChanged(item, start, end);
            }
        }

        /// <summary>
        /// 改变甘特图视图
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public async Task ChangeVieMode(string mode)
        {
            await InvokeVoidAsync("changeViewMode", Id, mode);
        }
    }
}
