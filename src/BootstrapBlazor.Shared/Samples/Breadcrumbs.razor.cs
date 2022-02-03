// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Breadcrumbs
{
    [NotNull]
    private IEnumerable<BreadcrumbItem>? DataSource { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        DataSource = new List<BreadcrumbItem>
            {
                new BreadcrumbItem("Home", "#"),
                new BreadcrumbItem("Library", "#"),
                new BreadcrumbItem("Data")
            };
    }
}
