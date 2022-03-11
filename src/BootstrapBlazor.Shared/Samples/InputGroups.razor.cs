// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class InputGroups
{
    private string BindValue { get; set; } = string.Empty;

    private string StringAt { get; set; } = "@";

    private string StringMailServer { get; set; } = "163.com";

    private readonly IEnumerable<SelectedItem> Items3 = new SelectedItem[]
    {
        new SelectedItem ("", "请选择 ..."),
        new SelectedItem ("Beijing", "北京"),
        new SelectedItem ("Shanghai", "上海")
    };
}
