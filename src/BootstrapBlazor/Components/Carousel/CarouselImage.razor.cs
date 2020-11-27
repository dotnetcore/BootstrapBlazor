// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class CarouselImage : ComponentBase
    {
        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string? ImageUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public Func<string, Task>? OnClick { get; set; }

        private async Task OnClickImage()
        {
            if (OnClick != null) await OnClick(ImageUrl ?? "");
        }
    }
}
