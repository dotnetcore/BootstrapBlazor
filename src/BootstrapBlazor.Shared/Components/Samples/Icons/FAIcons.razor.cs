// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples.Icons;

/// <summary>
/// 图标库
/// </summary>
public partial class FAIcons
{
    private bool ShowCopyDialog { get; set; }

    private string DisplayText => ShowCopyDialog ? Localizer["SwitchButtonTextOn"] : Localizer["SwitchButtonTextOff"];
}
