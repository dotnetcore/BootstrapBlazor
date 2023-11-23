// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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
