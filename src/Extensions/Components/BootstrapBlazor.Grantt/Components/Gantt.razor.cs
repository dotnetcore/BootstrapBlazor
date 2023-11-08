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
        /// 
        /// </summary>
        [Parameter]
#if NET6_0_OR_GREATER
        [EditorRequired]
#endif
        public IEnumerable<GanttItem>? Items { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public GanttMode ViewMode { get; set; } = GanttMode.DAY;

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<GanttItem, Task>? OnClick { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<GanttItem, string, string, Task>? OnDataChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<GanttItem, int, Task>? OnProgressChanged { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnViewClick { get; set; }

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
                await InvokeVoidAsync("init", Id, Items, ViewMode.ToDescriptionString(), Interop);
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
    }
}
