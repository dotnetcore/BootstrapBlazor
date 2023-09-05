// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public partial class DialButtons
{
    private DialMode Mode { get; set; }

    private Task OnClick(DialMode mode)
    {
        Mode = mode;
        StateHasChanged();
        return Task.CompletedTask;
    }

    private CheckboxState CheckState(string state) => Mode.ToString() == state ? CheckboxState.Checked : CheckboxState.UnChecked;

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new()
        {
            Name = nameof(DialButton.Placement),
            Description = "the dial button placement",
            Type = "Placement",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(DialButton.DialMode),
            Description = "the dial button placement",
            Type = "DialMode",
            ValueList = "Linear/Radial",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(DialButton.Radius),
            Description = "the dial popup radius",
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
