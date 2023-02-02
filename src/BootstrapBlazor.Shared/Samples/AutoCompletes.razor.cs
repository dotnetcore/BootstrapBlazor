// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
///
/// </summary>
public sealed partial class AutoCompletes
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
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
            Name = "ChildContent",
            Description = Localizer["Att2"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "ItemTemplate",
            Description = Localizer["AttItemTemplate"],
            Type = "RenderFragment<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "Items",
            Description = Localizer["Att3"],
            Type = "IEnumerable<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "NoDataTip",
            Description = Localizer["Att4"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["Att4DefaultValue"]!
        },
        new AttributeItem()
        {
            Name = "DisplayCount",
            Description = Localizer["Att5"],
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "ValueChanged",
            Description = Localizer["Att6"],
            Type = "Action<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "IsLikeMatch",
            Description = Localizer["Att7"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "IgnoreCase",
            Description = Localizer["Att8"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "OnCustomFilter",
            Description = Localizer["Att9"],
            Type = "Func<string, Task<IEnumerable<string>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "Debounce",
            Description = Localizer["Debounce"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new AttributeItem()
        {
            Name = nameof(AutoComplete.SkipEnter),
            Description = Localizer[nameof(AutoComplete.SkipEnter)],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = nameof(AutoComplete.SkipEsc),
            Description = Localizer[nameof(AutoComplete.SkipEsc)],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = nameof(AutoComplete.OnValueChanged),
            Description = Localizer[nameof(AutoComplete.OnValueChanged)],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(AutoComplete.OnSelectedItemChanged),
            Description = Localizer[nameof(AutoComplete.OnSelectedItemChanged)],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
