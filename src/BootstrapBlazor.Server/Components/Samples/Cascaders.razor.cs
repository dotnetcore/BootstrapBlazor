// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Cascader 组件示例代码
/// </summary>
public sealed partial class Cascaders
{
    /// <summary>
    /// Foo 类为Demo测试用，如有需要请自行下载源码查阅
    /// Foo class is used for Demo test, please download the source code if necessary
    /// https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/main/src/BootstrapBlazor.Server/Data/Foo.cs
    /// </summary>
    private Foo Model { get; set; } = new Foo();

    private Guid CurrentGuid { get; set; }

    private readonly IEnumerable<CascaderItem> _guidItems = new CascaderItem[]
    {
        new(Guid.NewGuid().ToString(), "Guid1"),
        new(Guid.NewGuid().ToString(), "Guid2")
    };

    private string Value { get; set; } = "Shanghai";

    [NotNull]
    private ConsoleLogger? NormalLogger { get; set; }

    [NotNull]
    private ValidateForm? ValidateForm1 { get; set; }

    private List<CascaderItem> _items = [];

    /// <summary>
    /// 下拉选项改变时调用此方法
    /// </summary>
    /// <param name="items"></param>
    private Task OnItemChanged(CascaderItem[] items)
    {
        NormalLogger.Log($"SelectedItem Text: {items[^1].Text} Value: {items[^1].Value} Selected");
        return Task.CompletedTask;
    }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        Value = Localizer["item1"];

        _items =
        [
            new CascaderItem(Localizer["item1"], Localizer["item1"]),
            new CascaderItem(Localizer["item2"], Localizer["item2"]),
            new CascaderItem(Localizer["item3"], Localizer["item3"]),
        ];

        _items[0].AddItem(new CascaderItem("item1_child1", Localizer["item1_child1"]));
        _items[0].AddItem(new CascaderItem("item1_child2", Localizer["item1_child2"]));
        _items[0].AddItem(new CascaderItem("item1_child3", Localizer["item1_child3"]));
        _items[0].AddItem(new CascaderItem("item1_child4", Localizer["item1_child4"]));

        _items[0].Items.ElementAt(0).AddItem(new CascaderItem("item1_child1_child", Localizer["item1_child1_child"]));

        _items[1].AddItem(new CascaderItem("item2_child1", Localizer["item2_child1"]));
        _items[1].AddItem(new CascaderItem("item2_child2", Localizer["item2_child2"]));
        _items[1].AddItem(new CascaderItem("item2_child3", Localizer["item2_child3"]));

        _items[2].AddItem(new CascaderItem("item3_child1", Localizer["item3_child1"]));
        _items[2].AddItem(new CascaderItem("item3_child2", Localizer["item3_child2"]));
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private EventItem[] GetEvents() =>
    [
        new()
        {
            Name = nameof(Cascader<string>.OnSelectedItemChanged),
            Description = Localizer["Event1"],
            Type ="Func<CascaderItem[], Task>"
        }
    ];

    private async Task OnValidate()
    {
        await ValidateForm1.ValidateAsync();
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "ShowLabel",
            Description = Localizer["Att1"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "DisplayText",
            Description = Localizer["Att2"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "PlaceHolder",
            Description = Localizer["Att3"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["Att3Default"]!
        },
        new()
        {
            Name = "Class",
            Description = Localizer["Att4"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Color",
            Description = Localizer["Att5"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new()
        {
            Name = "IsDisabled",
            Description = Localizer["Att6"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Items",
            Description = Localizer["Att7"],
            Type = "IEnumerable<CascaderItem>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
