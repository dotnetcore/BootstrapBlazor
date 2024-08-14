// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.Icons;

/// <summary>
/// 图标库
/// </summary>
public partial class FAIcons
{
    private bool ShowCopyDialog { get; set; }

    private string DisplayText => ShowCopyDialog ? Localizer["SwitchButtonTextOn"] : Localizer["SwitchButtonTextOff"];
}
