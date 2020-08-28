using System;
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

            if (firstRender && Tooltip != null)
            {
                await JSRuntime.Tooltip(Id, "", Tooltip.PopoverType, RetrieveTitle(), RetrieveContent(), RetrieveIsHtml(), RetrieveTrigger());
            }
        }

        /// <summary>
        /// 获得 弹窗标题方法
        /// </summary>
        /// <returns></returns>
        protected virtual string RetrieveTitle()
        {
            return Tooltip?.Title ?? "";
        }

        /// <summary>
        /// 获得 弹窗内容方法
        /// </summary>
        /// <returns></returns>
        protected virtual string RetrieveContent()
        {
            return Tooltip?.Content ?? "";
        }

        /// <summary>
        /// 获得 弹窗内容是否为 Html 方法
        /// </summary>
        /// <returns></returns>
        protected virtual bool RetrieveIsHtml()
        {
            return Tooltip?.IsHtml ?? false;
        }

        /// <summary>
        /// 获得 弹窗激活方法
        /// </summary>
        /// <returns></returns>
        protected virtual string RetrieveTrigger()
        {
            return Tooltip?.Trigger ?? "hover focus";
        }

        /// <summary>
        /// Dispose 方法
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && Tooltip != null)
            {
                _ = JSRuntime.Tooltip(Id, "dispose", popoverType: Tooltip.PopoverType);
            }
        }
    }
}
