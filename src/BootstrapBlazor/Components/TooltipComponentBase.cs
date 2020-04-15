using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 提供 Tooltip 功能的组件
    /// </summary>
    public abstract class TooltipComponentBase : IdComponentBase, ITooltipHost, IDisposable
    {
        /// <summary>
        /// 获得/设置 ITooltip 实例
        /// </summary>
        public ITooltip? Tooltip { get; set; }

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
                // 初始化 Tooltip 组件
                // 调用客户端 Tooltip 方法
                if (Tooltip != null)
                {
                    if (AdditionalAttributes == null) AdditionalAttributes = new Dictionary<string, object>();
                    AdditionalAttributes["data-placement"] = Tooltip.Placement.ToDescriptionString();
                    await InvokeAsync(StateHasChanged).ConfigureAwait(false);

                    // 增加一个延时保证客户端生成 Id
                    await Task.Delay(150);
                }
            }

            if (Tooltip != null) InvokeTooltip(firstRender);
        }

        /// <summary>
        /// 调用 Tooltip 脚本方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <remarks>OnAfterRenderAsync 方法内部调用此方法</remarks>
        protected virtual void InvokeTooltip(bool firstRender)
        {
            if (firstRender && Tooltip != null)
            {
                JSRuntime.Tooltip(Id, "", Tooltip.PopoverType, RetrieveTitle(), RetrieveContent(), Tooltip.IsHtml);
            }
        }

        private string RetrieveTitle()
        {
            return Tooltip != null ? Tooltip.Title : "";
        }

        private string RetrieveContent()
        {
            return Tooltip != null ? (Tooltip.PopoverType == PopoverType.Popover ? Tooltip.Content : "") : "";
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && Tooltip != null)
            {
                JSRuntime.Tooltip(Id, "dispose", popoverType: Tooltip.PopoverType);
            }
        }
    }
}
