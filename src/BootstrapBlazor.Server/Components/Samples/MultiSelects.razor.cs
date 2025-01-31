// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// MultiSelects
/// </summary>
public partial class MultiSelects
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo tet, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    private Foo Model { get; set; } = new Foo();

    private Foo Foo { get; set; } = new Foo();

    [NotNull]
    private List<SelectedItem>? EditableItems { get; set; }

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

    private int[] SelectedIntArrayValues { get; set; } = [];

    [NotNull]
    private List<SelectedItem>? Items { get; set; }

    [NotNull]
    private List<SelectedItem>? DataSource { get; set; }

    private string SelectedItemsValue { get; set; } = "Beijing";

    private IEnumerable<string> SelectedArrayValues { get; set; } = [];

    private IEnumerable<EnumEducation> SelectedEnumValues { get; set; } = new List<EnumEducation>
    {
        EnumEducation.Middle, EnumEducation.Primary
    };

    private MultiSelectEnumFoo EnumFoo { get; set; } = MultiSelectEnumFoo.One | MultiSelectEnumFoo.Two;

    [Flags]
    private enum MultiSelectEnumFoo
    {
        One = 1,
        Two = 2,
        Three = 4,
        Four = 8
    }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

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

    private List<SelectedItem> CascadingItems1 { get; set; } = [];

    private string? _editString;

    private async Task<SelectedItem> OnEditCallback(string value)
    {
        await Task.Delay(100);

        var item = EditableItems.Find(i => i.Text.Equals(value, StringComparison.OrdinalIgnoreCase));
        if (item == null)
        {
            item = new SelectedItem(value, value);
            EditableItems.Add(item);
        }
        return item;
    }

    private SelectedItem[] GroupItems { get; } =
    [
        new("Jilin", "吉林") { GroupName = "东北"},
        new("Liaoning", "辽宁") {GroupName = "东北", Active = true },
        new("Beijing", "北京") { GroupName = "华中"},
        new("Shijiazhuang", "石家庄") { GroupName = "华中"},
        new("Shanghai", "上海") {GroupName = "华东", Active = true },
        new("Ningbo", "宁波") {GroupName = "华东", Active = true }
    ];

    private readonly SelectedItem[] _cascadingItems2 =
    [
        new("", "请选择 ..."),
        new("Beijing", "北京") { Active = true },
        new("Shanghai", "上海"),
        new("Hangzhou", "杭州")
    ];

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
        EditableItems = GenerateItems();

        // 初始化数据
        DataSource =
        [
            new("Beijing", "北京"),
            new("Shanghai", "上海"),
            new("Guangzhou", "广州")
        ];

        LongDataSource =
        [
            new("1", "特别甜的东瓜(特别甜的东瓜)"),
            new("2", "特别甜的西瓜(特别甜的西瓜)"),
            new("3", "特别甜的南瓜(特别甜的南瓜)"),
            new("4", "特别甜的傻瓜(特别甜的傻瓜)"),
            new("5", "特别甜的金瓜(特别甜的金瓜)"),
            new("6", "特别甜的木瓜(特别甜的木瓜)"),
            new("7", "特别甜的水瓜(特别甜的水瓜)"),
            new("8", "特别甜的火瓜(特别甜的火瓜)"),
            new("9", "特别甜的土瓜(特别甜的土瓜)"),
        ];

        LongItems = GenerateDataSource(LongDataSource);

        Items = GenerateDataSource(DataSource);
    }

    private static List<SelectedItem> GenerateItems() =>
    [
        new ("Beijing", "北京"),
        new ("Shanghai", "上海"),
        new ("Guangzhou", "广州"),
        new ("Shenzhen", "深圳"),
        new ("Chengdu", "成都"),
        new ("Wuhan", "武汉"),
        new ("Dalian", "大连"),
        new ("Hangzhou", "杭州"),
        new ("Lianyungang", "连云港")
    ];

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
        SelectedArrayValues = ["Beijing"];
    }

    private void ClearListItems()
    {
        SelectedArrayValues = [];
    }

    private void AddArrayItems()
    {
        SelectedIntArrayValues = [1, 2, 3, 4];
    }

    private void RemoveArrayItems()
    {
        SelectedIntArrayValues = [1, 2,];
    }

    private void ClearArrayItems()
    {
        SelectedIntArrayValues = [];
    }

    private IEnumerable<SelectedItem> OnSearch(string searchText)
    {
        Logger.Log($"{Localizer["MultiSelectSearchLog"]}：{searchText}");
        SearchItemsSource ??= GenerateItems();
        return SearchItemsSource.Where(i => i.Text.Contains(searchText, StringComparison.OrdinalIgnoreCase));
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
            CascadingItems1 =
            [
                new("1","朝阳区") { Active = true },
                new("2","海淀区")
            ];
        }
        else if (item.Value == "Shanghai")
        {
            CascadingItems1 =
            [
                new("1","静安区"),
                new("2","黄浦区") {Active = true },
            ];
        }
        else
        {
            CascadingItems1 = [];
        }
        StateHasChanged();
    }

    /// <summary>
    /// 获得事件方法
    /// GetEvents
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
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
    ];

    /// <summary>
    /// 获得属性方法
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
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
            Name = nameof(MultiSelect<string>.IsSingleLine),
            Description = Localizer["MultiSelectsAttribute_IsSingleLine"],
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
        },
        new()
        {
            Name = "IsFixedHeight",
            Description = Localizer["MultiSelectsAttribute_IsFixedHeight"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    ];
}
