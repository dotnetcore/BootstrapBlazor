// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class GoTop
    {
        private ElementReference GoTopElement { get; set; }

        /// <summary>
        /// 获得/设置 滚动条所在组件
        /// </summary>
        [Parameter]
        public string? Target { get; set; }

        /// <summary>
        /// 获得/设置 鼠标悬停提示文字信息
        /// </summary>
        [Parameter]
        [NotNull]
        public string? TooltipText { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<GoTop>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            TooltipText ??= Localizer[nameof(TooltipText)];
        }

        /// <summary>
        /// OnAfterRenderAsync 方法
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && !string.IsNullOrEmpty(Target)) await JSRuntime.InvokeVoidAsync(GoTopElement, "footer", Target);
        }
    }
}
