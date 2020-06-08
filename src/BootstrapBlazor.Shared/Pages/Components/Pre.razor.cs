using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Pages.Components
{
    /// <summary>
    /// Pre 组件
    /// </summary>
    public sealed partial class Pre
    {
        private ElementReference? PreElement { get; set; }

        /// <summary>
        /// 获得 样式集合
        /// </summary>
        /// <returns></returns>
        private string? ClassName => CssBuilder.Default()
            .AddClassFromAttributes(AdditionalAttributes)
            .Build();

        [Inject]
        private ExampleService? Example { get; set; }

        /// <summary>
        /// 获得/设置 IJSRuntime 实例
        /// </summary>
        [Inject]
        private IJSRuntime? JSRuntime { get; set; }

        /// <summary>
        /// 获得/设置 子组件 CodeFile 为空时生效
        /// </summary>
        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        /// <summary>
        /// 获得/设置 用户自定义属性
        /// </summary>
        /// <returns></returns>
        [Parameter(CaptureUnmatchedValues = true)]
        public IDictionary<string, object>? AdditionalAttributes { get; set; }

        /// <summary>
        /// 获得/设置 示例文档名称
        /// </summary>
        [Parameter]
        public string? CodeFile { get; set; }

        /// <summary>
        /// 获得/设置 代码加载后回调委托
        /// </summary>
        [Parameter]
        public Func<string, Task<string>>? OnAfterLoadCode { get; set; }

        /// <summary>
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            await ReloadExampleCodeAsync();
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            JSRuntime.InvokeVoidAsync("$.highlight", PreElement);
        }

        private async Task ReloadExampleCodeAsync()
        {
            if (Example != null && !string.IsNullOrEmpty(CodeFile))
            {
                var code = await Example.GetCodeAsync(CodeFile);
                if (OnAfterLoadCode != null) code = await OnAfterLoadCode.Invoke(code);

                if (!string.IsNullOrEmpty(code))
                {
                    ChildContent = builder =>
                    {
                        var index = 0;
                        builder.AddContent(index++, code);
                    };
                }
            }
        }
    }
}
