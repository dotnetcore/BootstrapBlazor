// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Rendering;

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">TableFormatContent 组件</para>
/// <para lang="en">TableFormatContent Component</para>
/// </summary>
internal class TableFormatContent : ComponentBase
{
    /// <summary>
    /// <para lang="zh">获得/设置 格式化方法</para>
    /// <para lang="en">Gets or sets formatter method</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public Func<object?, Task<string?>>? Formatter { get; set; }

    /// <summary>
    /// <para lang="zh">获得/设置 当前显示数据项</para>
    /// <para lang="en">Gets or sets current display data item</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    [EditorRequired]
    public object? Item { get; set; }

    private string? _content;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        _content = await Formatter(Item);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="builder"></param>
    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (!string.IsNullOrEmpty(_content))
        {
            builder.AddContent(0, _content);
        }
    }
}
