// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

using System.Reflection.Metadata;

namespace BootstrapBlazor.Components;

/// <summary>
/// Live2DDisplay Live2D组件
/// </summary>
[JSModuleAutoLoader("./_content/BootstrapBlazor.Live2DDisplay/Components/Live2DDisplay/Live2DDisplay.razor.js")]
public partial class Live2DDisplay
{
    /// <summary>
    /// 获得/设置 Source 模型源
    /// </summary>
    [Parameter]
    public string? Source { get; set; }

    /// <summary>
    /// 获得/设置 Scale 比例
    /// </summary>
    [Parameter]
    public double Scale { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public int X { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public int Y { get; set; }


    private string? ClassString => CssBuilder.Default().AddClassFromAttributes(AdditionalAttributes).Build();

    /// <inheritdoc/>
    protected override async Task OnParametersSetAsync()
    {
        if (!string.IsNullOrEmpty(Source))
        {
            await InvokeVoidAsync("reload", Id, Source, Scale, X, Y);
        }
    }

    /// <inheritdoc/>
    protected override async Task InvokeInitAsync()
    {
        await InvokeVoidAsync("init", Id, Source, Scale, X, Y);
    }
}
