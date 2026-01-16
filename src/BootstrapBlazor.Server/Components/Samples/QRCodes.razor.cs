// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// QRCodes
/// </summary>
public sealed partial class QRCodes
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task OnGenerated()
    {
        Logger.Log(Localizer["SuccessText"]);
        return Task.CompletedTask;
    }

    private Task OnCleared()
    {
        Logger.Log(Localizer["ClearText"]);
        return Task.CompletedTask;
    }
}
