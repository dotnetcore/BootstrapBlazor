// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Tags
/// </summary>
public sealed partial class Tags
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task DismissClick()
    {
        Logger.Log($"Tag Dismissed");
        return Task.CompletedTask;
    }
}
