// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.DockViews2;

/// <summary>
/// DockViewLayout 组件
/// </summary>
public partial class DockViewLayout
{
    [NotNull]
    private DockView? DockView { get; set; }

    private Task Reset() => DockView.Reset();

    private void OnToggleLayout1()
    {
        LayoutConfig = LayoutConfig1;
    }

    private void OnToggleLayout2()
    {
        LayoutConfig = LayoutConfig2;
    }

    private void OnToggleLayout3()
    {
        LayoutConfig = LayoutConfig3;
    }

    private string? LayoutConfig = "";

    const string LayoutConfig1 = "";

    const string LayoutConfig2 = "";

    const string LayoutConfig3 = "";
}
