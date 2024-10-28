// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Breadcrumbs 组件示例
/// </summary>
public partial class Breadcrumbs
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
