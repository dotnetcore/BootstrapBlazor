// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples.Table;

/// <summary>
/// 
/// </summary>
public partial class TablesNameDrop
{
    /// <summary>
    /// 
    /// </summary>
    [Parameter]
    [NotNull]
    public Foo? Model { get; set; }

    private List<SelectedItem> Items { get; } =
    [
        new SelectedItem { Text = "请选择 ...", Value = "" },
        new SelectedItem { Text = "自定义姓名1", Value = "自定义姓名1" },
        new SelectedItem { Text = "自定义姓名2", Value = "自定义姓名2" },
        new SelectedItem { Text = "自定义姓名3", Value = "自定义姓名3" },
        new SelectedItem { Text = "自定义姓名4", Value = "自定义姓名4" },
    ];
}
