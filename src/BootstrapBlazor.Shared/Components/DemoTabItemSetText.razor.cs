// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Components;

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
