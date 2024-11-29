// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

/// <summary>
/// EyeDroppers
/// </summary>
public partial class EyeDroppers
{
    [Inject]
    [NotNull]
    private EyeDropperService? EyeDropperService { get; set; }
    private string? Value { get; set; }
    private string ValueString => Value ?? "#fff";

    private async Task OnOpen()
    {
        Value = await EyeDropperService.PickAsync();
    }
}
