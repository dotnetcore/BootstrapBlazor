// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// Breadcrumb 组件
/// </summary>
public sealed partial class Breadcrumb
{
    /// <summary>
    /// 获得/设置 数据集
    /// </summary>
    [Parameter]
    public IEnumerable<BreadcrumbItem> Value { get; set; } = Enumerable.Empty<BreadcrumbItem>();

    private string? GetItemClassName(BreadcrumbItem item) => CssBuilder.Default("breadcrumb-item")
        .Build();

    private string? CurrentPage(BreadcrumbItem item) => CssBuilder.Default()
        .Build();
}
