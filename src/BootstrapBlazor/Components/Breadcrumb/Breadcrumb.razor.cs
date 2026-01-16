// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">Breadcrumb 组件</para>
/// <para lang="en">Breadcrumb component</para>
/// </summary>
public sealed partial class Breadcrumb
{
    /// <summary>
    /// <para lang="zh">获得/设置 数据集</para>
    /// <para lang="en">Gets or sets the data collection</para>
    /// <para><version>10.2.2</version></para>
    /// </summary>
    [Parameter]
    [NotNull]
    public IEnumerable<BreadcrumbItem>? Value { get; set; }

    private static string? GetItemClassString(BreadcrumbItem item) => CssBuilder.Default("breadcrumb-item")
        .AddClass(item.CssClass)
        .Build();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        Value ??= [];
    }
}
