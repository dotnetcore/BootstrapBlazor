// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// MultiSelects
/// </summary>
public partial class MultiSelects
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo tet, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Shared/Data/Foo.cs
    /// </summary>
    private Foo Model { get; set; } = new Foo();

    private Foo Foo { get; set; } = new Foo();

    [NotNull]
    private List<SelectedItem>? Items1 { get; set; }

    [NotNull]
    private List<SelectedItem>? Items2 { get; set; }

    [NotNull]
    private List<SelectedItem>? Items3 { get; set; }

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
    private List<SelectedItem>? LongItems { get; set; }

    [NotNull]
    private List<SelectedItem>? LongDataSource { get; set; }

    private int[] SelectedIntArrayValues { get; set; } = Array.Empty<int>();

    [NotNull]
    private List<SelectedItem>? Items { get; set; }

    [NotNull]
    private List<SelectedItem>? DataSource { get; set; }

    private string SelectedItemsValue { get; set; } = "Beijing";

    private IEnumerable<string> SelectedArrayValues { get; set; } = Enumerable.Empty<string>();

    private IEnumerable<EnumEducation> SelectedEnumValues { get; set; } = new List<EnumEducation>
    {
        EnumEducation.Middle, EnumEducation.Primary
    };

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private ConsoleLogger? OptionLogger { get; set; }

    private List<SelectedItem>? SearchItemsSource { get; set; }

    private string SelectedSearchItemsValue { get; set; } = "Beijing";

    private string SelectedDisableItemsValue { get; set; } = "Beijing";

    private string SelectedOptionItemsValue { get; set; } = "Beijing";

    private string SelectedLongItemsValue { get; set; } = "";

    private string SelectedLongItemsValue1 { get; set; } = "";

    private string SelectedMaxItemsValue { get; set; } = "";

    private string SelectedMinItemsValue { get; set; } = "";

    private string SelectedLongItemsValue3 { get; set; } = "";

    [NotNull]
    private IEnumerable<SelectedItem>? TemplateItems { get; set; }

    private List<SelectedItem> CascadingItems1 { get; set; } = new List<SelectedItem>();

    private IEnumerable<SelectedItem> GroupItems { get; } = new SelectedItem[]
    {
        new SelectedItem ("Jilin", "吉林") { GroupName = "东北"},
        new SelectedItem ("Liaoning", "辽宁") {GroupName = "东北", Active = true },
        new SelectedItem ("Beijing", "北京") { GroupName = "华中"},
        new SelectedItem ("Shijiazhuang", "石家庄") { GroupName = "华中"},
        new SelectedItem ("Shanghai", "上海") {GroupName = "华东", Active = true },
        new SelectedItem ("Ningbo", "宁波") {GroupName = "华东", Active = true }
    };

    private readonly List<SelectedItem> CascadingItems2 = new SelectedItem[]
    {
        new SelectedItem ("", "请选择 ..."),
        new SelectedItem ("Beijing", "北京") { Active = true },
        new SelectedItem ("Shanghai", "上海"),
        new SelectedItem ("Hangzhou", "杭州")
    }.ToList();

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items1 = GenerateItems();
        Items2 = GenerateItems();
        Items3 = GenerateItems();
        Items4 = GenerateItems();
        Items5 = GenerateItems();
        Items6 = GenerateItems();
        Items7 = GenerateItems();
        Items8 = GenerateItems();
        TemplateItems = GenerateItems();

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

        LongItems = GenerateDataSource(LongDataSource);

        Items = GenerateDataSource(DataSource);
    }

    private static List<SelectedItem> GenerateItems() => new()
    {
        new ("Beijing", "北京"),
        new ("Shanghai", "上海"),
        new ("Guangzhou", "广州"),
        new ("Shenzhen", "深圳"),
        new ("Chengdu", "成都"),
        new ("Wuhan", "武汉"),
        new ("Dalian", "大连"),
        new ("Hangzhou", "杭州"),
        new ("Lianyungang", "连云港")
    };

    private static List<SelectedItem> GenerateDataSource(List<SelectedItem> source) => source.Select(i => new SelectedItem(i.Value, i.Text)).ToList();

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

    private IEnumerable<SelectedItem> OnSearch(string searchText)
    {
        Logger.Log($"{Localizer["MultiSelectSearchLog"]}：{searchText}");
        SearchItemsSource ??= GenerateItems();
        return SearchItemsSource.Where(i => i.Text.Contains(searchText, System.StringComparison.OrdinalIgnoreCase));
    }

    private Task OnSelectedItemsChanged8(IEnumerable<SelectedItem> items)
    {
        Logger.Log($"{Localizer["MultiSelectOptionChangeLog"]}：{string.Join(", ", items.Select(i => i.Value))}");
        return Task.CompletedTask;
    }

    private static void OnClickButton()
    {
        //do something ...
        //your code
    }

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
            Items1 = new List<SelectedItem>(new[]
            {
                new SelectedItem("1","朝阳区") { Active = true },
                new SelectedItem("2","海淀区")
            });
        }
        else if (item.Value == "Shanghai")
        {
            Items1 = new List<SelectedItem>(new[]
            {
                new SelectedItem("1","静安区"),
                new SelectedItem("2","黄浦区") {Active = true },
            });
        }
        else
        {
            Items1 = new List<SelectedItem>();
        }
        StateHasChanged();
    }

    /// <summary>
    /// 获得事件方法
    /// GetEvents
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new[]
    {
        new EventItem()
        {
            Name = "OnSelectedItemsChanged",
            Description = Localizer["MultiSelectsEvent_OnSelectedItemsChanged"],
            Type = "Func<SelectedItem, Task>"
        },
        new EventItem()
        {
            Name = "OnSearchTextChanged",
            Description = Localizer["MultiSelectsEvent_OnSearchTextChanged"],
            Type = "Func<string, IEnumerable<SelectedItem>>"
        }
    };

    /// <summary>
    /// 获得属性方法
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        // TODO: 移动到数据库中
        new()
        {
            Name = "ShowLabel",
            Description = Localizer["MultiSelectsAttribute_ShowLabel"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowCloseButton",
            Description = Localizer["MultiSelectsAttribute_ShowCloseButton"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowToolbar",
            Description = Localizer["MultiSelectsAttribute_ShowToolbar"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "ShowDefaultButtons",
            Description = Localizer["MultiSelectsAttribute_ShowDefaultButtons"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "DisplayText",
            Description = Localizer["MultiSelectsAttribute_DisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "PlaceHolder",
            Description = Localizer["MultiSelectsAttribute_PlaceHolder"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["MultiSelectsAttribute_PlaceHolder_DefaultValue"]!
        },
        new()
        {
            Name = "Class",
            Description = Localizer["MultiSelectsAttribute_Class"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Color",
            Description = Localizer["MultiSelectsAttribute_Color"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["MultiSelectsAttribute_IsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Items",
            Description = Localizer["MultiSelectsAttribute_Items"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ButtonTemplate",
            Description = Localizer["MultiSelectsAttribute_ButtonTemplate"],
            Type = "RenderFragment<IEnumerable<SelectedItem>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ItemTemplate",
            Description = Localizer["MultiSelectsAttribute_ItemTemplate"],
            Type = "RenderFragment<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
