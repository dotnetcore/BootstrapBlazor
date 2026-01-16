// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Collapses
/// </summary>
public sealed partial class Collapses
{
    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    private Task OnChanged(CollapseItem item)
    {
        NormalLogger.Log($"{item.Text}: {item.IsCollapsed}");
        return Task.CompletedTask;
    }

    private bool State { get; set; }

    private void OnToggle()
    {
        State = !State;
    }

    private string? Value { get; set; }

    private IEnumerable<SelectedItem> Items { get; set; } = new[]
    {
        new SelectedItem ("Beijing", "北京"),
        new SelectedItem ("Shanghai", "上海") { Active = true },
    };

    private AttributeItem[] GetCollapseItemAttributes() =>
    [
        new()
        {
            Name = "Text",
            Description = Localizer["CollapseItemAttributeText"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Icon",
            Description = Localizer["CollapseItemAttributeIcon"],
            Type = "Func<CollapseItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "TitleColor",
            Description = Localizer["CollapseItemAttributeTitleColor"],
            Type = "Func<CollapseItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Class",
            Description = Localizer["CollapseItemAttributeClass"],
            Type = "Func<CollapseItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderClass",
            Description = Localizer["CollapseItemAttributeHeaderClass"],
            Type = "Func<CollapseItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = Localizer["CollapseItemAttributeHeaderTemplate"],
            Type = "Func<CollapseItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsCollapsed",
            Description = Localizer["CollapseItemAttributeIsCollapsed"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    ];
}
