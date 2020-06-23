using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

        /// <summary>
        /// 获得 Markdown 编辑器源码
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetMarkdownString()
        {
            var ret = "";
            if (JSRuntime != null) ret = await JSRuntime.InvokeAsync<string>(MarkdownElement, "markdown", "getMarkdown");
            return ret;
        }

        /// <summary>
        /// 获得 Markdown 编辑器 HTML 源码
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetMarkdownHtmlString()
        {
            var ret = "";
            if (JSRuntime != null) ret = await JSRuntime.InvokeAsync<string>(MarkdownElement, "markdown", "getHTML");
            return ret;
        }
    }
}
