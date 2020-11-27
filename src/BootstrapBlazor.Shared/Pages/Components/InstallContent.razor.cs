// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class InstallContent
    {
        /// <summary>
        /// 获得/设置 版本号字符串
        /// </summary>
        private string Version { get; set; } = "latest";

        /// <summary>
        ///
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "服务器端渲染模式";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public string HostFile { get; set; } = "Pages/_Host.cshtml";

        /// <summary>
        /// 
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            Version = await VersionManager.GetVersionAsync();
        }
    }
}
