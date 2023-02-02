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
        new AttributeItem()
        {
            Name = "ShowLabel",
            Description = Localizer["MultiSelectsAttribute_ShowLabel"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "ShowCloseButton",
            Description = Localizer["MultiSelectsAttribute_ShowCloseButton"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "ShowToolbar",
            Description = Localizer["MultiSelectsAttribute_ShowToolbar"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "ShowDefaultButtons",
            Description = Localizer["MultiSelectsAttribute_ShowDefaultButtons"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "DisplayText",
            Description = Localizer["MultiSelectsAttribute_DisplayText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "PlaceHolder",
            Description = Localizer["MultiSelectsAttribute_PlaceHolder"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["MultiSelectsAttribute_PlaceHolder_DefaultValue"]!
        },
        new AttributeItem()
        {
            Name = "Class",
            Description = Localizer["MultiSelectsAttribute_Class"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "Color",
            Description = Localizer["MultiSelectsAttribute_Color"],
            Type = "Color",
            ValueList = "Primary / Secondary / Success / Danger / Warning / Info / Dark",
            DefaultValue = "Primary"
        },
        new AttributeItem()
        {
            Name = "IsDisabled",
            Description = Localizer["MultiSelectsAttribute_IsDisabled"],
            Type = "boolean",
            ValueList = "true / false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "Items",
            Description = Localizer["MultiSelectsAttribute_Items"],
            Type = "IEnumerable<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "ButtonTemplate",
            Description = Localizer["MultiSelectsAttribute_ButtonTemplate"],
            Type = "RenderFragment<IEnumerable<SelectedItem>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "ItemTemplate",
            Description = Localizer["MultiSelectsAttribute_ItemTemplate"],
            Type = "RenderFragment<SelectedItem>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
