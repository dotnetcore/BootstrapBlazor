// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;

using System.Reflection.Metadata;

namespace BootstrapBlazor.Components;

/// <summary>
/// L2Dwidget 看板娘组件
/// </summary>
[JSModuleAutoLoader("./_content/BootstrapBlazor.L2Dwidget/Components/L2Dwidget/L2Dwidget.razor.js")]
public partial class L2Dwidget
{
    /// <summary>
    /// 获得/设置 L2DwidgetOptions
    /// </summary>
    [Parameter]
    public L2DwidgetOptions? Options { get; set; }

    /// <summary>
    /// 获得/设置 JsonPath 模型路径
    /// </summary>
    [Parameter]
    public string? JsonPath { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns></returns>
    protected override async Task InvokeInitAsync()
    {
        Options ??= new L2DwidgetOptions();
        if (!string.IsNullOrWhiteSpace(JsonPath))
        {
            Options.Model.JsonPath = JsonPath;
        }
        await InvokeVoidAsync("init", Options);
    }
}
