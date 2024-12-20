﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

internal class TableFormatContent : ComponentBase
{
    /// <summary>
    /// 获得/设置 格式化方法
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// 获得/设置 当前显示数据项
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public object? Item { get; set; }

    private string? _content;

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        _content = await Formatter(Item);
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (!string.IsNullOrEmpty(_content))
        {
            builder.AddContent(0, _content);
        }
    }
}
