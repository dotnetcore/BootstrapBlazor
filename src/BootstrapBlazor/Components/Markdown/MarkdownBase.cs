using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Markdown 基类
    /// </summary>
    public abstract class MarkdownBase : BootstrapComponentBase
    {
        /// <summary>
        /// 获得/设置 DOM 元素实例
        /// </summary>
        protected ElementReference MarkdownElement { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null) await JSRuntime.Invoke(MarkdownElement, "markdown");
        }
    }
}
