// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：Apache-2.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Block
    {
        private ElementReference BlockElement { get; set; }

        /// <summary>
        /// 获得/设置 组件 Title 属性
        /// </summary>
        [Parameter]
        [NotNull]
        public string? Title { get; set; }

        /// <summary>
        /// 获得/设置 组件说明信息
        /// </summary>
        [Parameter]
        public string Introduction { get; set; } = "未设置";

        /// <summary>
        /// 获得/设置 组件内容
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 组件示例代码文件名
        /// </summary>
        [Parameter]
        public string? CodeFile { get; set; }

        [NotNull]
        private string? SubTitle { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<Block>? Localizer { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Title ??= Localizer[nameof(Title)];
            SubTitle ??= Localizer[nameof(SubTitle)];
        }

        /// <summary>
        /// OnAfterRenderAsync
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender && JSRuntime != null)
            {
                await JSRuntime.InvokeVoidAsync("$.block", BlockElement);
            }
        }
    }
}
