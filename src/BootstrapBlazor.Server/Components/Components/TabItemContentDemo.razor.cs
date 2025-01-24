// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// TabItemContentDemo 组件
/// </summary>
public partial class TabItemContentDemo
{
    [CascadingParameter]
    [NotNull]
    private TabItem? TabItem { get; set; }

    private Task OnToggleDisable()
    {
        TabItem.SetDisabled(true);
        return Task.CompletedTask;
    }
}
