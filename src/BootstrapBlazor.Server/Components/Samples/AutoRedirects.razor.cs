// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// AutoRedirects
/// </summary>
public partial class AutoRedirects
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task<bool> OnBeforeRedirectAsync()
    {
        Logger.Log("Ready to redirect");
        return Task.FromResult(true);
    }

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
}
