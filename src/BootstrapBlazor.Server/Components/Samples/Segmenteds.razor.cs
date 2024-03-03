// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 
/// </summary>
public partial class Segmenteds
{
    private BootstrapBlazor.Components.ConsoleLogger? ConsoleLogger { get; set; }

    private List<SegmentedOption<string>> Items { get; } =
    [
        new() { Value = "Daily", Text = "Daily" },
        new() { Value = "Weekly", Text = "Weekly" },
        new() { Value = "Monthly", Text = "Monthly" },
        new() { Value = "Quarterly", Text = "Quarterly" },
        new() { Value = "Yearly", Text = "Yearly" }
    ];

    private List<SegmentedOption<string>> DisabledItems { get; } =
    [
        new() { Value = "123", Text = "123" },
        new() { Value = "456", Text = "456", IsDisabled = true },
        new() { Value = "789", Text = "789" },
        new() { Value = "000", Text = "long-text-long-text-long-text-long-text" }
    ];

    private List<SegmentedOption<string>> ItemTemplateItems { get; } =
    [
        new() { Value = "123", Text = "123", Icon = "fa-solid fa-flag" },
        new() { Value = "456", Text = "456", Icon = "fa-solid fa-flag" },
        new() { Value = "789", Text = "789", Icon = "fa-solid fa-flag" }
    ];

    private List<SegmentedOption<string>> IconItems { get; set; } =
    [
        new() { Value = "list", Text = "List", Icon = "fas fa-bars" },
        new() { Value = "chart", Text = "Chart", Icon = "fas fa-chart-column" }
    ];

    private List<SegmentedOption<string>> SizeItems { get; } =
    [
        new() { Value = "Daily", Text = "Daily" },
        new() { Value = "Weekly", Text = "Weekly" },
        new() { Value = "Monthly", Text = "Monthly" },
        new() { Value = "Quarterly", Text = "Quarterly" },
        new() { Value = "Yearly", Text = "Yearly" }
    ];

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task OnValueChanged(string value)
    {
        Logger.Log(value);
        return Task.CompletedTask;
    }

    private string Value { get; set; } = "Daily";

    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = nameof(Segmented<string>.Items),
            Description = Localizer["ItemsAttr"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Segmented<string>.Value),
            Description = Localizer["ValueChangedAttr"],
            Type = "TValue",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Segmented<string>.ValueChanged),
            Description = Localizer["ValueChangedAttr"],
            Type = "EventCallBack<SegmentedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Segmented<string>.OnValueChanged),
            Description = Localizer["ValueChangedAttr"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Segmented<string>.IsDisabled),
            Description = Localizer["IsDisabledAttr"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Segmented<string>.IsBlock),
            Description = Localizer["IsBlockAttr"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Segmented<string>.ShowTooltip),
            Description = Localizer["ShowTooltipAttr"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(Segmented<string>.Size),
            Description = Localizer["SizeAttr"],
            Type = "Size",
            ValueList = "None / ExtraSmall / Small / Medium / Large / ExtraLarge",
            DefaultValue = "None"
        },
        new()
        {
            Name = nameof(Segmented<string>.ItemTemplate),
            Description = Localizer["ItemTemplateAttr"],
            Type = "RenderFragment<SegmentedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(Segmented<string>.ChildContent),
            Description = Localizer["ChildContentAttr"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
