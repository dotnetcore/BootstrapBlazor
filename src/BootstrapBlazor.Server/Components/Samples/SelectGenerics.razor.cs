// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 下拉框操作类
/// </summary>
public sealed partial class SelectGenerics
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Foo Model { get; set; } = new Foo();

    private IEnumerable<SelectedItem<string?>> Items { get; set; } = new List<SelectedItem<string?>>()
    {
        new("Beijing", "北京"),
        new("Shanghai", "上海") { Active = true },
    };

    private IEnumerable<SelectedItem<string?>> ClearableItems { get; set; } = new List<SelectedItem<string?>>()
    {
        new("", "未选择"),
        new("Beijing", "北京"),
        new("Shanghai", "上海")
    };

    private IEnumerable<SelectedItem<Foo>> VirtualItems => Foos.Select(i => new SelectedItem<Foo>(i, i.Name!));

    private Foo VirtualItem1 { get; set; } = new();

    private Foo VirtualItem2 { get; set; } = new();

    [NotNull]
    private List<Foo>? Foos { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    private bool _showSearch;

    private bool _isShowSearchClearable;

    private bool _isClearable;

    private Foo _foo = new();

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        TimeZoneInfo.ClearCachedData();
        TimeZoneItems = TimeZoneInfo.GetSystemTimeZones().Select(i => new SelectedItem<string>(i.Id, i.DisplayName));
        TimeZoneId = TimeZoneInfo.Local.Id;
        TimeZoneValue = TimeZoneInfo.Local.BaseUtcOffset;
        Foos = Foo.GenerateFoo(FooLocalizer);
    }

    private async Task<QueryData<SelectedItem<Foo>>> OnQueryAsync(VirtualizeQueryOption option)
    {
        await Task.Delay(200);
        var items = Foos;
        if (!string.IsNullOrEmpty(option.SearchText))
        {
            items = Foos.Where(i => i.Name!.Contains(option.SearchText, StringComparison.OrdinalIgnoreCase)).ToList();
        }
        return new QueryData<SelectedItem<Foo>>
        {
            Items = items.Skip(option.StartIndex).Take(option.Count).Select(i => new SelectedItem<Foo>(i, i.Name!)),
            TotalCount = items.Count
        };
    }

    private Task OnItemChanged(SelectedItem<string?> item)
    {
        Logger.Log($"SelectedItem Text: {item.Text} Value: {item.Value} Selected");
        StateHasChanged();
        return Task.CompletedTask;
    }

    private readonly IEnumerable<SelectedItem<string>> Items4 = new SelectedItem<string>[]
    {
        new("Beijing", "北京") { IsDisabled = true},
        new("Shanghai", "上海") { Active = true },
        new("Guangzhou", "广州")
    };

    private Foo BindingModel { get; set; } = new Foo();

    private Foo ClearableModel { get; set; } = new Foo();

    private SelectedItem? Item { get; set; }

    private string ItemString => Item == null ? "" : $"{Item.Text} ({Item.Value})";

    private readonly IEnumerable<SelectedItem<string>> Items3 = new SelectedItem<string>[]
    {
        new("", "请选择 ..."),
        new("Beijing", "北京") { Active = true },
        new("Shanghai", "上海"),
        new("Hangzhou", "杭州")
    };

    private IEnumerable<SelectedItem<string>>? Items2 { get; set; }

    private Task OnShowDialog() => Dialog.Show(new DialogOption()
    {
        Title = "弹窗中使用级联下拉框",
        Component = BootstrapDynamicComponent.CreateComponent<CustomerSelectDialog>()
    });

    private async Task OnCascadeBindSelectClick(SelectedItem<string> item)
    {
        // 模拟异步通讯切换线程
        await Task.Delay(10);
        if (item.Value == "Beijing")
        {
            Items2 = new SelectedItem<string>[]
            {
                new("1","朝阳区") { Active = true},
                new("2","海淀区"),
            };
        }
        else if (item.Value == "Shanghai")
        {
            Items2 = new SelectedItem<string>[]
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

    private readonly IEnumerable<SelectedItem<string>> GroupItems = new SelectedItem<string>[]
    {
        new("Jilin", "吉林") { GroupName = "东北"},
        new("Liaoning", "辽宁") {GroupName = "东北", Active = true },
        new("Beijing", "北京") { GroupName = "华中"},
        new("Shijiazhuang", "石家庄") { GroupName = "华中"},
        new("Shanghai", "上海") {GroupName = "华东", Active = true },
        new("Ningbo", "宁波") {GroupName = "华东", Active = true }
    };

    private Guid CurrentGuid { get; set; }

    private readonly IEnumerable<SelectedItem<Guid>> GuidItems = new SelectedItem<Guid>[]
    {
        new(Guid.NewGuid(), "Guid1"),
        new(Guid.NewGuid(), "Guid2")
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
            item = new SelectedItem<string?>() { Value = v, Text = v };
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

    private IEnumerable<SelectedItem<int?>> NullableIntItems { get; set; } = new SelectedItem<int?>[]
    {
        new() { Text = "Item 1", Value = null },
        new() { Text = "Item 2", Value = 2 },
        new() { Text = "Item 3", Value = 3 }
    };

    private bool? SelectedBoolItem { get; set; }

    private string GetSelectedBoolItemString()
    {
        return SelectedBoolItem.HasValue ? SelectedBoolItem.Value.ToString() : "null";
    }

    private IEnumerable<SelectedItem<bool?>> NullableBoolItems { get; set; } = new SelectedItem<bool?>[]
    {
        new() { Text = "空值", Value = null },
        new() { Text = "True 值", Value = true },
        new() { Text = "False 值", Value = false }
    };

    private readonly SelectedItem<string>[] StringItems =
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

    private static Task<bool> OnBeforeSelectedItemChange(SelectedItem<string> item)
    {
        return Task.FromResult(true);
    }

    [NotNull]
    private IEnumerable<SelectedItem<string>>? TimeZoneItems { get; set; }

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

    private readonly List<SelectedItem<Foo>> _genericItems =
    [
        new() { Text = "Foo1", Value = new Foo() { Id = 1, Address = "Address_F001" } },
        new() { Text = "Foo2", Value = new Foo() { Id = 2, Address = "Address_F002" } },
        new() { Text = "Foo3", Value = new Foo() { Id = 3, Address = "Address_F003" } }
    ];

    private Foo _selectedFoo = new();

    private async Task<Foo> TextConvertToValueCallback(string v)
    {
        // 模拟异步通讯切换线程
        await Task.Delay(10);

        Foo? foo = null;
        var item = _genericItems.Find(i => i.Text == v);
        if (item == null)
        {
            var id = _genericItems.Count + 1;
            foo = new Foo() { Id = id, Address = $"New Address - {id}" };
            var fooItem = new SelectedItem<Foo> { Text = v, Value = foo };
            _genericItems.Add(fooItem);
        }
        else
        {
            foo = item.Value;
        }
        return foo!;
    }
}
