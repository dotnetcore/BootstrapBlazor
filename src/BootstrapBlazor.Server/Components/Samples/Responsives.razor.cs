// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Responsives
/// </summary>
public partial class Responsives
{
    private BreakPoint Size { get; set; }

    private Task OnChanged(BreakPoint size)
    {
        Size = size;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(Responsive.OnBreakPointChanged),
            Description = "Callback method when breakpoint threshold changes",
            Type = "Func<BreakPoint, Task<bool>>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
