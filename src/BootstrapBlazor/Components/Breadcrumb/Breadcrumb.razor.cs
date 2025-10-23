﻿// Licensed to the .NET Foundation under one or more agreements.
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
