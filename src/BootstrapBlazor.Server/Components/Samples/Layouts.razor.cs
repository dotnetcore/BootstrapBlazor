// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Layout 组件示例
/// </summary>
public sealed partial class Layouts
{
    private List<MenuItem>? IconSideMenuItems1 { get; set; }

    private List<MenuItem>? IconSideMenuItems2 { get; set; }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    /// <returns></returns>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        IconSideMenuItems1 = await MenusDataGenerator.GetIconSideMenuItemsAsync(LocalizerMenu);
        IconSideMenuItems2 = await MenusDataGenerator.GetIconSideMenuItemsAsync(LocalizerMenu);
    }
}
