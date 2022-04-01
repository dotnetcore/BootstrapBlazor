// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
///
/// </summary>
public partial class MultiSelects
{
    [NotNull]
    private BlockLogger? Trace { get; set; }

    [NotNull]
    private BlockLogger? Trace2 { get; set; }

    [NotNull]
    private List<SelectedItem>? DataSource { get; set; }

    [NotNull]
    private List<SelectedItem>? LongDataSource { get; set; }

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        // 初始化数据
        DataSource = new List<SelectedItem>
        {
            new SelectedItem ("Beijing", "北京"),
            new SelectedItem ("Shanghai", "上海"),
            new SelectedItem ("Guangzhou", "广州")
        };

        LongDataSource = new List<SelectedItem>
        {
            new SelectedItem ("1", "特别甜的东瓜(特别甜的东瓜)"),
            new SelectedItem ("2", "特别甜的西瓜(特别甜的西瓜)"),
            new SelectedItem ("3", "特别甜的南瓜(特别甜的南瓜)"),
            new SelectedItem ("4", "特别甜的傻瓜(特别甜的傻瓜)"),
            new SelectedItem ("5", "特别甜的金瓜(特别甜的金瓜)"),
            new SelectedItem ("6", "特别甜的木瓜(特别甜的木瓜)"),
            new SelectedItem ("7", "特别甜的水瓜(特别甜的水瓜)"),
            new SelectedItem ("8", "特别甜的火瓜(特别甜的火瓜)"),
            new SelectedItem ("9", "特别甜的土瓜(特别甜的土瓜)"),
        };

        Items4 = GenerateDataSource(DataSource);
        Items5 = GenerateDataSource(DataSource);
        Items6 = GenerateDataSource(DataSource);
        Items7 = GenerateDataSource(DataSource);
        Items8 = GenerateDataSource(DataSource);
        Items9 = GenerateDataSource(DataSource);
        Items10 = GenerateDataSource(DataSource);
        Items11 = GenerateDataSource(DataSource);

        LongItems = GenerateDataSource(LongDataSource);
        LongItems1 = GenerateDataSource(LongDataSource);
        LongItems2 = GenerateDataSource(LongDataSource);
        LongItems3 = GenerateDataSource(LongDataSource);
        LongItems4 = GenerateDataSource(LongDataSource);
        LongItems5 = GenerateDataSource(LongDataSource);
    }

    private static List<SelectedItem> GenerateDataSource(List<SelectedItem> source) => source.Select(i => new SelectedItem(i.Value, i.Text)).ToList();

    /// <summary>
    /// 级联绑定菜单
    /// </summary>
    /// <param name="item"></param>
    private async Task OnCascadeBindSelectClick(SelectedItem item)
    {
        // 模拟异步获取数据源
        await Task.Delay(100);
        if (item.Value == "Beijing")
        {
            Items2 = new List<SelectedItem>(new[]
            {
                new SelectedItem("1","朝阳区") { Active = true },
                new SelectedItem("2","海淀区")
            });
        }
        else if (item.Value == "Shanghai")
        {
            Items2 = new List<SelectedItem>(new[]
            {
                new SelectedItem("1","静安区"),
                new SelectedItem("2","黄浦区") {Active = true },
            });
        }
        else
        {
            Items2 = new List<SelectedItem>();
        }
        StateHasChanged();
    }

    private void AddItems()
    {
        SelectedItemsValue = "Beijing,Shanghai,Guangzhou";
    }

    private void RemoveItems()
    {
        SelectedItemsValue = "Beijing";
    }

    private void ClearItems()
    {
        SelectedItemsValue = "";
    }

    private void AddListItems()
    {
        SelectedArrayValues = "Beijing,Shanghai".Split(',');
    }

    private void RemoveListItems()
    {
        SelectedArrayValues = new[] { "Beijing" };
    }

    private void ClearListItems()
    {
        SelectedArrayValues = Enumerable.Empty<string>();
    }

    private void AddArrayItems()
    {
        SelectedIntArrayValues = new[] { 1, 2, 3, 4 };
    }

    private void RemoveArrayItems()
    {
        SelectedIntArrayValues = new[] { 1, 2, };
    }

    private void ClearArrayItems()
    {
        SelectedIntArrayValues = Array.Empty<int>();
    }

    private int[] SelectedIntArrayValues { get; set; } = Array.Empty<int>();
    private IEnumerable<string> SelectedArrayValues { get; set; } = Enumerable.Empty<string>();
    private IEnumerable<EnumEducation> SelectedEnumValues { get; set; } = new List<EnumEducation> { EnumEducation.Middel, EnumEducation.Primary };

    private IEnumerable<SelectedItem> OnSearch(string searchText)
    {
        Trace.Log($"{Localizer["Log1"]}：{searchText}");
        return Items.Where(i => i.Text.Contains(searchText, System.StringComparison.OrdinalIgnoreCase));
    }

    private Task OnSelectedItemsChanged8(IEnumerable<SelectedItem> items)
    {
        Trace2.Log($"选中项集合：{string.Join(",", items.Select(i => i.Value))}");
        return Task.CompletedTask;
    }

    private Foo Model { get; set; } = new Foo();

    private Foo Foo { get; set; } = new Foo();

    private static void OnClickButton()
    {

    }

    private string SelectedLongItemsValue1 { get; set; } = "";

    private string SelectedLongItemsValue2 { get; set; } = "";

    private string SelectedLongItemsValue3 { get; set; } = "";

    private string SelectedMaxItemsValue { get; set; } = "";

    private string SelectedMinItemsValue { get; set; } = "";

    private string SelectedItemsValue { get; set; } = "Beijing";

    private string SelectedItemsValue6 { get; set; } = "Beijing";

    private string SelectedItemsValue7 { get; set; } = "Beijing";

    private string SelectedItemsValue8 { get; set; } = "Beijing";

    private IEnumerable<SelectedItem> Items { get; set; } = new[]
    {
        new SelectedItem ("Beijing", "北京"),
        new SelectedItem ("Shanghai", "上海"),
        new SelectedItem ("Guangzhou", "广州"),
        new SelectedItem ("Shenzhen", "深圳"),
        new SelectedItem ("Chengdu", "成都"),
        new SelectedItem ("Wuhan", "武汉"),
        new SelectedItem ("Dalian", "大连"),
        new SelectedItem ("Hangzhou", "杭州"),
        new SelectedItem ("Lianyungang", "连云港")
    };

    private List<SelectedItem> Items2 { get; set; } = new List<SelectedItem>();

    private readonly List<SelectedItem> Items3 = new SelectedItem[]
    {
        new SelectedItem ("", "请选择 ..."),
        new SelectedItem ("Beijing", "北京") { Active = true },
        new SelectedItem ("Shanghai", "上海"),
        new SelectedItem ("Hangzhou", "杭州")
    }.ToList();

    [NotNull]
    private List<SelectedItem>? Items4 { get; set; }

    [NotNull]
    private List<SelectedItem>? Items5 { get; set; }

    [NotNull]
    private List<SelectedItem>? Items6 { get; set; }

    [NotNull]
    private List<SelectedItem>? Items7 { get; set; }

    [NotNull]
    private List<SelectedItem>? Items8 { get; set; }

    [NotNull]
    private List<SelectedItem>? Items9 { get; set; }

    [NotNull]
    private List<SelectedItem>? Items10 { get; set; }

    [NotNull]
    private List<SelectedItem>? Items11 { get; set; }

    [NotNull]
    private List<SelectedItem>? LongItems { get; set; }

    [NotNull]
    private List<SelectedItem>? LongItems1 { get; set; }

    [NotNull]
    private List<SelectedItem>? LongItems2 { get; set; }

    [NotNull]
    private List<SelectedItem>? LongItems3 { get; set; }

    [NotNull]
    private List<SelectedItem>? LongItems4 { get; set; }

    [NotNull]
    private List<SelectedItem>? LongItems5 { get; set; }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new[]
    {
        new EventItem()
        {
            Name = "OnSelectedItemChanged",
            Description = Localizer["Event1"],
            Type = "Func<SelectedItem, Task>"
        },
        new EventItem()
        {
            Name = "OnSearchTextChanged",
            Description = Localizer["Event2"],
            Type = "Func<string, IEnumerable<SelectedItem>>"
        }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        // TODO: 移动到数据库中
        new AttributeItem()
        {
            Name = "ShowLabel",
            Description = Localizer["Att1"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "ShowCloseButton",
            Description = Localizer["Att2"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "ShowToolbar",
            Description = Localizer["Att3"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "ShowDefaultButtons",
            Description = Localizer["Att4"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "DisplayText",
            Description = Localizer["Att5"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "PlaceHolder",
            Description = Localizer["Att6"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["Att1DefaultValue"]!
        },
        new AttributeItem()
        {
            Name = "Class",
            Description = Localizer["Att7"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "Color",
            Description = Localizer["Att8"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new AttributeItem()
        {
            Name = "IsDisabled",
            Description = Localizer["Att9"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "Items",
            Description = Localizer["Att10"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "ButtonTemplate",
            Description = Localizer["Att11"],
            Type = "RenderFragment<IEnumerable<SelectedItem>>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
