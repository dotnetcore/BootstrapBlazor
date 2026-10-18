// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Layout;

/// <summary>
/// Documentation shell with an Auto navigation menu.
/// </summary>
public partial class MainLayout
{
    /// <summary>
    /// Prevents rendering the shell again inside the server page boundary.
    /// </summary>
    [CascadingParameter(Name = "ServerPageContent")]
    public bool ServerPageContent { get; set; }
}
