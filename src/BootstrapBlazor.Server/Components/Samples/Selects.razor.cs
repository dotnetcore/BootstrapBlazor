// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 下拉框操作类
/// </summary>
public sealed partial class Selects
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Foo Model { get; set; } = new Foo();

    private IEnumerable<SelectedItem> Items { get; set; } = new[]
    {
        new SelectedItem ("Beijing", "北京"),
        new SelectedItem ("Shanghai", "上海") { Active = true },
    };

    private IEnumerable<SelectedItem> ClearableItems { get; set; } = new[]
    {
        new SelectedItem ("", "未选择"),
        new SelectedItem ("Beijing", "北京"),
        new SelectedItem ("Shanghai", "上海")
    };

    private IEnumerable<SelectedItem> VirtualItems => Foos.Select(i => new SelectedItem(i.Name!, i.Name!)).ToList();

    private SelectedItem? VirtualItem1 { get; set; }

    private SelectedItem? VirtualItem2 { get; set; }

    [NotNull]
    private List<Foo>? Foos { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    private bool _showSearch;

    private bool _showPopoverSearch = true;

    private bool _isShowSearchClearable;

    private bool _isClearable;

    private string? _fooName;

    private List<SelectedItem> _enumValueDemoItems = [
        new("0", "Primary"),
        new("1", "Middle")
    ];

    private EnumEducation _enumValueDemo = EnumEducation.Primary;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        TimeZoneInfo.ClearCachedData();
        TimeZoneItems = TimeZoneInfo.GetSystemTimeZones().Select(i => new SelectedItem(i.Id, i.DisplayName));
        TimeZoneId = TimeZoneInfo.Local.Id;
        TimeZoneValue = TimeZoneInfo.Local.BaseUtcOffset;
        Foos = Foo.GenerateFoo(LocalizerFoo);
    }

    private async Task<QueryData<SelectedItem>> OnQueryAsync(VirtualizeQueryOption option)
    {
        await Task.Delay(200);
        var items = Foos;
        if (!string.IsNullOrEmpty(option.SearchText))
        {
            items = [.. Foos.Where(i => i.Name!.Contains(option.SearchText, StringComparison.OrdinalIgnoreCase))];
        }
        return new QueryData<SelectedItem>
        {
            Items = items.Skip(option.StartIndex).Take(option.Count).Select(i => new SelectedItem(i.Name!, i.Name!)),
            TotalCount = items.Count
        };
    }

    private Task OnItemChanged(SelectedItem item)
    {
        Logger.Log($"SelectedItem Text: {item.Text} Value: {item.Value} Selected");
        StateHasChanged();
        return Task.CompletedTask;
    }

    private readonly IEnumerable<SelectedItem> Items4 = new SelectedItem[]
    {
        new("Beijing", "北京") { IsDisabled = true},
        new("Shanghai", "上海") { Active = true },
        new("Guangzhou", "广州")
    };

    private Foo BindingModel { get; set; } = new Foo();

    private Foo ClearableModel { get; set; } = new Foo();

    private SelectedItem? Item { get; set; }

    private string ItemString => Item == null ? "" : $"{Item.Text} ({Item.Value})";

    private readonly IEnumerable<SelectedItem> Items3 = new SelectedItem[]
    {
        new("", "请选择 ..."),
        new("Beijing", "北京") { Active = true },
        new("Shanghai", "上海"),
        new("Hangzhou", "杭州")
    };

    private IEnumerable<SelectedItem>? Items2 { get; set; }

    private Task OnShowDialog() => Dialog.Show(new DialogOption()
    {
        Title = "弹窗中使用级联下拉框",
        Component = BootstrapDynamicComponent.CreateComponent<CustomerSelectDialog>()
    });

    private async Task OnCascadeBindSelectClick(SelectedItem item)
    {
        // 模拟异步通讯切换线程
        await Task.Delay(10);
        if (item.Value == "Beijing")
        {
            Items2 = new SelectedItem[]
            {
                new("1","朝阳区") { Active = true},
                new("2","海淀区"),
            };
        }
        else if (item.Value == "Shanghai")
        {
            Items2 = new SelectedItem[]
            {
                new("1","静安区"),
                new("2","黄浦区") { Active = true } ,
            };
        }
        else
        {
            Items2 = [];
        }
        StateHasChanged();
    }

    private Foo ValidateModel { get; set; } = new Foo() { Name = "" };

    private readonly IEnumerable<SelectedItem> GroupItems = new SelectedItem[]
    {
        new("Jilin", "吉林") { GroupName = "东北"},
        new("Liaoning", "辽宁") {GroupName = "东北", Active = true },
        new("Beijing", "北京") { GroupName = "华中"},
        new("Shijiazhuang", "石家庄") { GroupName = "华中"},
        new("Shanghai", "上海") {GroupName = "华东", Active = true },
        new("Ningbo", "宁波") {GroupName = "华东", Active = true }
    };

    private Guid CurrentGuid { get; set; }

    private readonly IEnumerable<SelectedItem> GuidItems = new SelectedItem[]
    {
        new(Guid.NewGuid().ToString(), "Guid1"),
        new(Guid.NewGuid().ToString(), "Guid2")
    };

    private Foo LabelModel { get; set; } = new Foo();

    private EnumEducation SelectedEnumItem { get; set; } = EnumEducation.Primary;

    private EnumEducation? SelectedEnumItem1 { get; set; }

    private int? NullableSelectedIntItem { get; set; }

    private Task OnInputChangedCallback(string v)
    {
        var item = Items.FirstOrDefault(i => i.Text.Equals(v, StringComparison.OrdinalIgnoreCase));
        if (item == null)
        {
            item = new SelectedItem() { Value = v, Text = v };
            var items = Items.ToList();
            items.Insert(0, item);
            Items = items;
        }
        return Task.CompletedTask;
    }

    private string GetSelectedIntItemString()
    {
        return NullableSelectedIntItem.HasValue ? NullableSelectedIntItem.Value.ToString() : "null";
    }

    private IEnumerable<SelectedItem> NullableIntItems { get; set; } = new SelectedItem[]
    {
        new() { Text = "Item 1", Value = "" },
        new() { Text = "Item 2", Value = "2" },
        new() { Text = "Item 3", Value = "3" }
    };

    private bool? SelectedBoolItem { get; set; }

    private string GetSelectedBoolItemString()
    {
        return SelectedBoolItem.HasValue ? SelectedBoolItem.Value.ToString() : "null";
    }

    private IEnumerable<SelectedItem> NullableBoolItems { get; set; } = new SelectedItem[]
    {
        new() { Text = "空值", Value = "" },
        new() { Text = "True 值", Value = "True" },
        new() { Text = "False 值", Value = "False" }
    };

    private readonly SelectedItem[] StringItems =
    [
        new("1", "1"),
        new("12", "12"),
        new("123", "123"),
        new("1234", "1234"),
        new("a", "a"),
        new("ab", "ab"),
        new("abc", "abc"),
        new("abcd", "abcd"),
        new("abcde", "abcde")
    ];

    private static Task<bool> OnBeforeSelectedItemChange(SelectedItem item)
    {
        return Task.FromResult(true);
    }

    [NotNull]
    private IEnumerable<SelectedItem>? TimeZoneItems { get; set; }

    private string? TimeZoneId { get; set; }

    [NotNull]
    private TimeSpan TimeZoneValue { get; set; }

    private Task OnTimeZoneValueChanged(string timeZoneId)
    {
        TimeZoneId = timeZoneId;
        TimeZoneValue = TimeZoneInfo.GetSystemTimeZones().First(i => i.Id == timeZoneId).BaseUtcOffset;
        StateHasChanged();
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = "OnSelectedItemChanged",
            Description = Localizer["SelectsOnSelectedItemChanged"],
            Type = "Func<SelectedItem, Task>"
        },
        new()
        {
            Name = "OnBeforeSelectedItemChange",
            Description = Localizer["SelectsOnBeforeSelectedItemChange"],
            Type = "Func<SelectedItem, Task<bool>>"
        },
        new()
        {
            Name = "OnInputChangedCallback",
            Description = Localizer["SelectsOnInputChangedCallback"],
            Type = "Func<string, Task>"
        },
        new()
        {
            Name = "TextConvertToValueCallback",
            Description = Localizer["SelectsTextConvertToValueCallback"],
            Type = "Func<string, Task<TValue>>"
        }
    ];

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "ShowLabel",
            Description = Localizer["SelectsShowLabel"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowSearch",
            Description = Localizer["SelectsShowSearch"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "DisplayText",
            Description = Localizer["SelectsDisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Class",
            Description = Localizer["SelectsClass"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Color",
            Description = Localizer["SelectsColor"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "IsEditable",
            Description = Localizer["SelectsIsEditable"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["SelectsIsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Items",
            Description = Localizer["SelectsItems"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "SelectItems",
            Description = Localizer["SelectItems"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ItemTemplate",
            Description = Localizer["SelectsItemTemplate"],
            Type = "RenderFragment<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ChildContent",
            Description = Localizer["SelectsChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Category",
            Description = Localizer["SelectsCategory"],
            Type = "SwalCategory",
            ValueList = " — ",
            DefaultValue = " SwalCategory.Information "
        },
        new()
        {
            Name = "Content",
            Description = Localizer["SelectsContent"],
            Type = "string?",
            ValueList = " — ",
            DefaultValue = Localizer["SelectsContentDefaultValue"]!
        },
        new()
        {
            Name = "DisableItemChangedWhenFirstRender",
            Description = Localizer["SelectsDisableItemChangedWhenFirstRender"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        }
    ];
}
