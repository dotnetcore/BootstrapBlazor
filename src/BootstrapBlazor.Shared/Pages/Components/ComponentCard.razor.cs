using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ComponentCard
    {
        private string ImageUrl => $"_content/BootstrapBlazor.Shared/images/{ImageName}";

        /// <summary>
        /// 获得/设置 Header 文字
        /// </summary>
        [Parameter]
        public string HeaderText { get; set; } = "未设置";

        /// <summary>
        /// 获得/设置 组件图片
        /// </summary>
        [Parameter]
        public string ImageName { get; set; } = "Divider.svg";

        /// <summary>
        /// 获得/设置 链接地址
        /// </summary>
        [Parameter]
        public string? Url { get; set; }
    }
}
