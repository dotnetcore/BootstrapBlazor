// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class Stacks
{
    private bool IsRow => ShowModeString == "true";

    private StackJustifyContent Justify { get; set; }

    private StackAlignItems AlignItems { get; set; }

    private StackJustifyContent JustifySelf { get; set; }

    private StackAlignItems AlignSelf { get; set; }

    private bool IsWrap { get; set; }

    private bool IsReverse { get; set; }

    private List<SelectedItem> ShowMode =>
    [
        new("true", Localizer["RowMode"]),
        new("false", Localizer["ColumnMode"])
    ];

    private string ShowModeString { get; set; } = "true";

    private IEnumerable<AttributeItem> GetAttributeItems() => new List<AttributeItem>()
    {
        new()
        {
            Name = nameof(Stack.IsRow),
            Description = Localizer["AttrIsRow"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Stack.IsWrap),
            Description = Localizer["AttrIsWrap"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Stack.IsReverse),
            Description = Localizer["AttrIsReverse"],
            Type = "bool",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Stack.Justify),
            Description = Localizer["AttrJustify"],
            Type = "StackJustifyContent",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Stack.AlignItems),
            Description = Localizer["AttrAlignItems"],
            Type = "StackAlignItems",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Stack.ChildContent),
            Description = Localizer["AttrChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
    };
}
