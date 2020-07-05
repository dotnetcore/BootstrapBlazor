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
        private string Version { get; set; } = "fetching";

        /// <summary>
        ///
        /// </summary>
        [Parameter]
        public string Title { get; set; } = "服务器端渲染模式";

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
