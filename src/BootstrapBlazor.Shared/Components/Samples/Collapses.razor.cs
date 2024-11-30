// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Shared.Components.Samples;

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

    private string Value { get; set; }

    private IEnumerable<SelectedItem> Items { get; set; } = new[]
    {
        new SelectedItem ("Beijing", "北京"),
        new SelectedItem ("Shanghai", "上海") { Active = true },
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "CollapseItems",
            Description = Localizer["CollapseItems"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsAccordion",
            Description = Localizer["IsAccordion"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "OnCollapseChanged",
            Description = Localizer["OnCollapseChanged"],
            Type = "Func<CollapseItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
