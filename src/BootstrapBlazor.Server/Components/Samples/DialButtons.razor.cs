// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// DialButton 组件示例代码
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
    private static AttributeItem[] GetAttributes() =>
    [
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
    ];
}
