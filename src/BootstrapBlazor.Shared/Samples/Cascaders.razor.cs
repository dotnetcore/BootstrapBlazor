// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Cascaders
{
    private string Value { get; set; } = "Shanghai";

    /// <summary>
    /// Default dataset
    /// </summary>
    private IEnumerable<CascaderItem> Items = new List<CascaderItem>();
    private Guid CurrentGuid { get; set; }

    /// <summary>
    /// OnInitialised method
    /// </summary>
    protected override void OnInitialized()
    {
        Value = Localizer["Location1"];
        Items = GetItems();
    }

    /// <summary>
    ///
    /// </summary>
    private Foo Model { get; set; } = new Foo();

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private IEnumerable<CascaderItem> GetItems()
    {
        var ret = new List<CascaderItem>
        {
            new CascaderItem(Localizer["Location1"], Localizer["Location1"]),
            new CascaderItem(Localizer["Location2"], Localizer["Location2"]),
            new CascaderItem(Localizer["Location3"], Localizer["Location3"]),
        };

        ret[0].AddItem(new CascaderItem("L1_CI1", Localizer["L1_CI1"]));
        ret[0].AddItem(new CascaderItem("L1_CI2", Localizer["L1_CI2"]));
        ret[0].AddItem(new CascaderItem("L1_CI3", Localizer["L1_CI3"]));
        ret[0].AddItem(new CascaderItem("L1_CI4", Localizer["L1_CI4"]));

        ret[0].Items.ElementAt(0).AddItem(new CascaderItem("CI5", Localizer["CI5"]));

        ret[1].AddItem(new CascaderItem("L2_CI1", Localizer["L2_CI1"]));
        ret[1].AddItem(new CascaderItem("L2_CI2", Localizer["L2_CI2"]));
        ret[1].AddItem(new CascaderItem("L2_CI3", Localizer["L2_CI3"]));

        ret[2].AddItem(new CascaderItem("L3_CI1", Localizer["L3_CI1"]));
        ret[2].AddItem(new CascaderItem("L3_CI2", Localizer["L3_CI2"]));

        return ret;
    }

    private readonly IEnumerable<CascaderItem> GuidItems = new CascaderItem[]
    {
        new CascaderItem(Guid.NewGuid().ToString(), "Guid1"),
        new CascaderItem(Guid.NewGuid().ToString(), "Guid2")
    };

    /// <summary>
    /// 下拉选项改变时调用此方法
    /// </summary>
    /// <param name="items"></param>
    private Task OnItemChanged(CascaderItem[] items)
    {
        Trace.Log($"SelectedItem Text: {items[^1].Text} Value: {items[^1].Value} Selected");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
        new EventItem()
        {
            Name = nameof(Cascader<string>.OnSelectedItemChanged),
            Description = Localizer["Event1"],
            Type ="Func<CascaderItem[], Task>"
        }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "ShowLabel",
            Description = Localizer["Att1"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "DisplayText",
            Description = Localizer["Att2"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "PlaceHolder",
            Description = Localizer["Att3"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["Att3Default"]!
        },
        new AttributeItem() {
            Name = "Class",
            Description = Localizer["Att4"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Color",
            Description = Localizer["Att5"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new AttributeItem() {
            Name = "IsDisabled",
            Description = Localizer["Att6"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["Att7"],
            Type = "IEnumerable<CascaderItem>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
