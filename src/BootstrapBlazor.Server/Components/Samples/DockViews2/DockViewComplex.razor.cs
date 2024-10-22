// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.DockViews2;

/// <summary>
/// DockViewComplex 示例
/// </summary>
public partial class DockViewComplex
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DockViewComplex>? Localizer { get; set; }
}
