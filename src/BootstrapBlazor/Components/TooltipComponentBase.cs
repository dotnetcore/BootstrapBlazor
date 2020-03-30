using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 提供 Tooltip 功能的组件
    /// </summary>
    public abstract class TooltipComponentBase : BootstrapComponentBase, ITooltipHost
    {
        /// <summary>
        /// 获得/设置 ITooltip 实例
        /// </summary>
        public ITooltip? Tooltip { get; set; }

        /// <summary>
        /// 获得 IJSRuntime 实例
        /// </summary>
        [Inject]
        protected IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// OnAfterRenderAsync
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && Tooltip != null)
            {
                // 初始化 Tooltip 组件
                // 调用客户端 Tooltip 方法
                if (string.IsNullOrEmpty(Id)) Id = await JSRuntime.GetClientIdAsync();

                if (AdditionalAttributes == null) AdditionalAttributes = new Dictionary<string, object>();
                AdditionalAttributes["title"] = Tooltip.Title;
                AdditionalAttributes["data-placement"] = Tooltip.Placement.ToDescriptionString();
                await InvokeAsync(StateHasChanged).ConfigureAwait(false);

                JSRuntime.Tooltip(Id);
            }
        }
    }
}
