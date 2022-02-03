// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;
using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Transfers : ComponentBase
{
    /// <summary>
    /// 
    /// </summary>
    [NotNull]
    private IEnumerable<SelectedItem>? Items { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items1 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items2 { get; set; }

    [NotNull]
    private List<SelectedItem>? Items3 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items4 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Items5 { get; set; }

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private List<SelectedItem> SelectedValue { get; set; } = new();

    private Foo Model { get; set; } = new();

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        // 模拟异步加载数据源
        await Task.Delay(100);

        Items = Enumerable.Range(1, 15).Select(i => new SelectedItem()
        {
            Text = $"{Localizer["Data"]} {i:d2}",
            Value = i.ToString()
        });

        Items1 = Enumerable.Range(1, 15).Select(i => new SelectedItem()
        {
            Text = $"{Localizer["Data"]} {i:d2}",
            Value = i.ToString()
        });

        Items2 = Enumerable.Range(1, 15).Select(i => new SelectedItem()
        {
            Text = $"{Localizer["Data"]} {i:d2}",
            Value = i.ToString()
        });

        Items3 = Enumerable.Range(1, 15).Select(i => new SelectedItem()
        {
            Text = $"{Localizer["Backup"]} {i:d2}",
            Value = i.ToString()
        }).ToList();

        SelectedValue.AddRange(Items3.Take(2));
        SelectedValue.AddRange(Items3.Skip(4).Take(1));

        Items4 = Enumerable.Range(1, 15).Select(i => new SelectedItem()
        {
            Text = $"{Localizer["Data"]} {i:d2}",
            Value = i.ToString()
        });

        Items5 = Enumerable.Range(1, 15).Select(i => new SelectedItem()
        {
            Text = $"{Localizer["Data"]} {i:d2}",
            Value = i.ToString()
        });
    }

    private void OnAddItem()
    {
        var count = Items3.Count + 1;
        Items3.Add(new SelectedItem(count.ToString(), $"{Localizer["Backup"]} {count:d2}"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="items"></param>
    private Task OnSelectedItemsChanged(IEnumerable<SelectedItem> items)
    {
        Trace?.Log(string.Join(" ", items.Select(i => i.Text)));
        return Task.CompletedTask;
    }

    private static string? SetItemClass(SelectedItem item) => item.Value switch
    {
        "2" => "bg-success text-white",
        "4" => "bg-info text-white",
        "6" => "bg-primary text-white",
        "8" => "bg-warning text-white",
        _ => null
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "Items",
                Description = Localizer["Items"],
                Type = "IEnumerable<SelectedItem>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "LeftButtonText",
                Description = Localizer["LeftButtonTextAttr"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "LeftPanelText",
                Description = Localizer["LeftPanelTextAttr"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["LeftPanelDefaultValue"]!
            },
            new AttributeItem() {
                Name = "RightButtonText",
                Description = Localizer["RightButtonTextAttr"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RightPanelText",
                Description = Localizer["RightPanelTextAttr"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["RightPanelTextDefaultValue"]!
            },
            new AttributeItem() {
                Name = "ShowSearch",
                Description = "",
                Type = "boolean",
                ValueList = " — ",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "LeftPannelSearchPlaceHolderString",
                Description = Localizer["LeftPannelSearchPlaceHolderString"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "RightPannelSearchPlaceHolderString",
                Description = Localizer["RightPannelSearchPlaceHolderString"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "IsDisabled",
                Description = Localizer["IsDisabled"],
                Type = "boolean",
                ValueList = "true / false",
                DefaultValue = "false"
            }
    };

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
            new EventItem()
            {
                Name = "OnItemsChanged",
                Description = Localizer["OnItemsChanged"],
                Type = "Action<IEnumerable<SelectedItem>>"
            },
            new EventItem()
            {
                Name = "OnSetItemClass",
                Description = Localizer["OnSetItemClass"],
                Type = "Func<SelectedItem, string?>"
            }
    };
}
