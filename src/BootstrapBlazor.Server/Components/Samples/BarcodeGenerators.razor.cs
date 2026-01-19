// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// BarcodeGenerators
/// </summary>
public partial class BarcodeGenerators
{
    private BarcodeGeneratorOption Options { get; set; } = new();

    private string Value { get; set; } = "12345";

    private string? _svgString;

    private Task OnCompletedAsync(string? val)
    {
        _svgString = val;
        StateHasChanged();
        return Task.CompletedTask;
    }
}
