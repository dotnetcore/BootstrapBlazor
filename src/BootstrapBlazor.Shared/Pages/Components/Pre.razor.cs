using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        /// <summary>
        /// 获得/设置 组件呈现内容
        /// </summary>
        private string? Content { get; set; }

        [Inject]
        private HttpClient? Client { get; set; }

        [Inject]
        private NavigationManager? Navigator { get; set; }

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
        /// OnInitializedAsync 方法
        /// </summary>
        /// <returns></returns>
        protected override async Task OnInitializedAsync()
        {
            if (Client != null && Navigator != null && !string.IsNullOrEmpty(CodeFile))
            {
                var baseUri = new Uri(Navigator.BaseUri);
                Client.BaseAddress = baseUri;

                try
                {
                    var folder = CodeFile.Split('.').FirstOrDefault();
                    if (!string.IsNullOrEmpty(folder))
                    {
                        Content = await Client.GetStringAsync($"_content/BootstrapBlazor.Docs/docs/{folder}/{CodeFile}");
                    }
                }
                catch (Exception ex)
                {
                    Content = ex.ToString();
                }

                if (!string.IsNullOrEmpty(Content))
                {
                    ChildContent = builder =>
                    {
                        var index = 0;
                        builder.AddContent(index++, Content);
                    };
                }
            }
        }

        /// <summary>
        /// OnAfterRender 方法
        /// </summary>
        /// <param name="firstRender"></param>
        protected override void OnAfterRender(bool firstRender)
        {
            JSRuntime.InvokeVoidAsync("$.highlight", PreElement);
        }
    }
}
