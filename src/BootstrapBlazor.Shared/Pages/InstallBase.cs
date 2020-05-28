using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages
{
    /// <summary>
    /// Install 组件基类
    /// </summary>
    public abstract class InstallBase : ComponentBase
    {
        [Inject]
        private NugetVersionService? VersionManager { get; set; }

        /// <summary>
        /// 获得/设置 版本号字符串
        /// </summary>
        protected string Version { get; set; } = "fetching";

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (VersionManager != null)
                Version = await VersionManager.GetVersionAsync();
        }
    }
}
