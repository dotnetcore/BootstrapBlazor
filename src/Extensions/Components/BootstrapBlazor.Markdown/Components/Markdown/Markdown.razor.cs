// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// Markdown 基类
    /// </summary>
    public sealed partial class Markdown
    {
        /// <summary>
        /// 获得/设置 DOM 元素实例
        /// </summary>
        private ElementReference MarkdownElement { get; set; }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender) await JSRuntime.InvokeVoidAsync("$.markdown", MarkdownElement);
        }

        /// <summary>
        /// 获得 Markdown 编辑器源码
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetMarkdownString() => await JSRuntime.InvokeAsync<string>("$.markdown", MarkdownElement, "getMarkdown");

        /// <summary>
        /// 获得 Markdown 编辑器 HTML 源码
        /// </summary>
        /// <returns></returns>
        public async ValueTask<string> GetMarkdownHtmlString() => await JSRuntime.InvokeAsync<string>("$.markdown", MarkdownElement, "getHTML");
    }
}
