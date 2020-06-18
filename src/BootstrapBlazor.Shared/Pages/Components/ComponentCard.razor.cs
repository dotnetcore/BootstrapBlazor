using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class ComponentCard
    {
        private string ImageUrl => $"_content/BootstrapBlazor.Shared/images/{ImageName}";

        private string? ClassString => CssBuilder.Default("form-group col-12 col-sm-6 col-md-4 col-lg-3")
            .AddClass("d-none", !string.IsNullOrEmpty(SearchText) && !HeaderText.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase))
            .Build();

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

        /// <summary>
        /// 
        /// </summary>
        [CascadingParameter]
        private List<string>? ComponentNames { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [CascadingParameter]
        private string? SearchText { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ComponentNames?.Add(HeaderText);
        }
    }
}
