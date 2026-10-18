// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Layout;

/// <summary>
/// Shared static shell for interactive page content.
/// </summary>
public partial class BaseLayout
{
    /// <summary>
    /// Prevents rendering the shell again inside the server page boundary.
    /// </summary>
    [CascadingParameter(Name = "ServerPageContent")]
    public bool ServerPageContent { get; set; }
}
