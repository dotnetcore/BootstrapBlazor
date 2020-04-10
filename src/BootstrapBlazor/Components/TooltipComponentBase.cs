using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 提供 Tooltip 功能的组件
    /// </summary>
    public abstract class TooltipComponentBase : BootstrapComponentBase, ITooltipHost, IDisposable
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

            if (firstRender)
            {
                // 生成 Id
                var invoke = false;
                if (string.IsNullOrEmpty(Id))
                {
                    Id = await JSRuntime.GetClientIdAsync();
                    invoke = true;
                }

                // 初始化 Tooltip 组件
                // 调用客户端 Tooltip 方法
                if (Tooltip != null)
                {
                    if (AdditionalAttributes == null) AdditionalAttributes = new Dictionary<string, object>();
                    AdditionalAttributes["title"] = Tooltip.Title;
                    AdditionalAttributes["data-placement"] = Tooltip.Placement.ToDescriptionString();
                    if (Tooltip.IsHtml) AdditionalAttributes["data-html"] = "true";
                    if (Tooltip.PopoverType == PopoverType.Popover)
                    {
                        AdditionalAttributes["data-content"] = Tooltip.Content;
                    }
                    invoke = true;
                }
                if (invoke) await InvokeAsync(StateHasChanged).ConfigureAwait(false);
            }

            InvokeTooltip(firstRender);
        }

        /// <summary>
        /// 调用 Tooltip 脚本方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <remarks>OnAfterRenderAsync 方法内部调用此方法</remarks>
        protected virtual void InvokeTooltip(bool firstRender)
        {
            if (firstRender) JSRuntime.Tooltip(Id, popoverType: Tooltip?.PopoverType ?? PopoverType.Tooltip);
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                JSRuntime.Tooltip(Id, "dispose", popoverType: Tooltip?.PopoverType ?? PopoverType.Tooltip);
            }
        }
    }
}
