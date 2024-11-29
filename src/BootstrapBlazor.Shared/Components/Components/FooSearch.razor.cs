// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Components;

/// <summary>
/// 
/// </summary>
public partial class FooSearch
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public FooSearchModel? Value { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    public EventCallback<FooSearchModel> ValueChanged { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<SelectedItem> CountItems { get; } =
    [
        new SelectedItem("", "全部"),
        new SelectedItem("1", "小于 30"),
        new SelectedItem("2", "大于等于 30 小于 70"),
        new SelectedItem("3", "大于等于 70 小于 100")
    ];
}
