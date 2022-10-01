// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BootstrapBlazor.Shared.Components;

/// <summary>
/// Pre 组件
/// </summary>
public partial class Pre
{
    private bool Loaded { get; set; }

    private bool CanCopy { get; set; }

    /// <summary>
    /// 获得 样式集合
    /// </summary>
    /// <returns></returns>
    private string? ClassName => CssBuilder.Default()
        .AddClassFromAttributes(AdditionalAttributes)
        .Build();

    [Inject]
    [NotNull]
    private CodeSnippetService? Example { get; set; }

    /// <summary>
    /// 获得/设置 子组件 CodeFile 为空时生效
    /// </summary>
    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    /// <summary>
    /// 获得/设置 示例文档名称
    /// </summary>
    [Parameter]
    public string? CodeFile { get; set; }

    /// <summary>
    /// 获得/设置 代码段的标题
    /// </summary>
    [Parameter]
    public string? BlockTitle { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        if (ChildContent == null)
        {
            await GetCodeAsync();
        }
        else
        {
            Loaded = true;
            CanCopy = true;
        }
    }

    /// <summary>
    /// OnAfterRender 方法
    /// </summary>
    /// <param name="firstRender"></param>
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (Loaded)
        {
            await JSRuntime.InvokeVoidAsync("$.highlight", $"#{Id}");
        }
    }

    private async Task GetCodeAsync()
    {
        if (!string.IsNullOrEmpty(CodeFile))
        {
            var code = await Example.GetCodeAsync(CodeFile, BlockTitle);
            if (!string.IsNullOrEmpty(code))
            {
                ChildContent = builder =>
                {
                    builder.AddContent(0, code);
                };
            }
            CanCopy = !string.IsNullOrEmpty(code) && !code.StartsWith("Error: ");
        }
        Loaded = true;
    }
}
