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
public sealed partial class Cascaders
{
    private string Value { get; set; } = "Shanghai";

    /// <summary>
    ///
    /// </summary>
    private Foo Model { get; set; } = new Foo();

    [NotNull]
    private BlockLogger? Trace { get; set; }

    private static IEnumerable<CascaderItem> GetItems()
    {
        var ret = new List<CascaderItem>
            {
                new CascaderItem("Beijing", "北京"),
                new CascaderItem("Shanghai", "上海"),
                new CascaderItem("GuangZhou", "广州"),
            };

        ret[0].AddItem(new CascaderItem("DC", "东城区"));
        ret[0].AddItem(new CascaderItem("XC", "西城区"));
        ret[0].AddItem(new CascaderItem("CY", "朝阳区"));
        ret[0].AddItem(new CascaderItem("CW", "崇文区"));

        ret[0].Items.ElementAt(0).AddItem(new CascaderItem("X", "某某街道"));

        ret[1].AddItem(new CascaderItem("HP", "黄浦区"));
        ret[1].AddItem(new CascaderItem("XH", "徐汇区"));

        return ret;
    }

    /// <summary>
    /// 获得 默认数据集合
    /// </summary>
    private readonly IEnumerable<CascaderItem> Items = GetItems();


    private Guid CurrentGuid { get; set; }

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
