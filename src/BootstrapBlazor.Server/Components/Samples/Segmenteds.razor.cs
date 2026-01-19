// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

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
}
