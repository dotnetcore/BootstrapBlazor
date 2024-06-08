// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.DockViews2;

/// <summary>
/// DockViewRow 示例
/// </summary>
public partial class DockViewRow
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DockViewRow>? Localizer { get; set; }
}
