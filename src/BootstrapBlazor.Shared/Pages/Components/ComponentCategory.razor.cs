// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ComponentCategory
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string Text { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string Desc { get; set; } = "";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public int Count { get; set; }
    }
}
