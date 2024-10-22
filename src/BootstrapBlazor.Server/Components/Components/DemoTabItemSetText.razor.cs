// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Components;

/// <summary>
/// DemoTabItem 组件
/// </summary>
public partial class DemoTabItemSetText
{
    [CascadingParameter]
    [NotNull]
    private TabItem? TabItem { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<DemoTabItem>? Localizer { get; set; }

    private void OnClick()
    {
        TabItem.SetHeader(DateTime.Now.ToString("mm:ss"));
    }
}
