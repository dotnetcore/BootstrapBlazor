// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Transfers
/// </summary>
public sealed partial class Transfers : ComponentBase
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

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

    private Foo Model { get; set; } = new();

    [NotNull]
    private IEnumerable<SelectedItem>? Items6 { get; set; }

    private Foo Model6 { get; set; } = new();

    [NotNull]
    private IEnumerable<SelectedItem>? Items7 { get; set; }

    private IEnumerable<SelectedItem> SelectedValue { get; set; } = Enumerable.Empty<SelectedItem>();

    private Task OnSelectedItemsChanged(IEnumerable<SelectedItem> items)
    {
        Logger.Log(string.Join(" ", items.Select(i => i.Text)));
        return Task.CompletedTask;
    }

    /// <summary>
    /// OnInitializedAsync 方法
    /// </summary>
    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        // 模拟异步加载数据源
        await Task.Delay(100);

        Items = GeneratorItems();
        Items1 = GeneratorItems();
        Items2 = GeneratorItems();

        Items3 = Enumerable.Range(1, 15).Select(i => new SelectedItem()
        {
            Text = $"{Localizer["Backup"]} {i:d2}",
            Value = i.ToString()
        }).ToList();

        Items4 = GeneratorItems();
        Items5 = GeneratorItems();
        Items6 = GeneratorItems();
        Items7 = GeneratorItems();

        var v = SelectedValue.ToList();
        v.AddRange(Items.Take(2));
        v.AddRange(Items.Skip(4).Take(1));
        SelectedValue = v;
    }

    private void OnAddItem()
    {
        var count = Items3.Count + 1;
        Items3.Add(new SelectedItem(count.ToString(), $"{Localizer["Backup"]} {count:d2}"));
    }

    private IEnumerable<SelectedItem> GeneratorItems() => Enumerable.Range(1, 15).Select(i => new SelectedItem()
    {
        Text = $"{Localizer["Data"]} {i:d2}",
        Value = i.ToString()
    });

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
        },
        new AttributeItem() {
            Name = "LeftHeaderTemplate",
            Description = Localizer["LeftHeaderTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "LeftItemTemplate",
            Description = Localizer["LeftItemTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "RightHeaderTemplate",
            Description = Localizer["RightHeaderTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "RightItemTemplate",
            Description = Localizer["RightItemTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
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
            Name = nameof(Transfer<string>.OnSelectedItemsChanged),
            Description = Localizer["OnSelectedItemsChanged"],
            Type = "Func<IEnumerable<SelectedItem>, Task>"
        },
        new EventItem()
        {
            Name = "OnSetItemClass",
            Description = Localizer["OnSetItemClass"],
            Type = "Func<SelectedItem, string?>"
        }
    };
}
