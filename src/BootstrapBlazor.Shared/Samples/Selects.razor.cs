// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 下拉框操作类
/// </summary>
public sealed partial class Selects
{
    [NotNull]
    private List<Foo>? Items { get; set; }

    private IEnumerable<SelectedItem> VirtualItems => Items.Select(i => new SelectedItem(i.Name!, i.Name!)).ToList();

    private SelectedItem? VirtualItem1 { get; set; }

    private SelectedItem? VirtualItem2 { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(LocalizerFoo);
    }

    private async Task<QueryData<SelectedItem>> OnQueryData(int startIndex, int length)
    {
        await Task.Delay(200);

        return new QueryData<SelectedItem>
        {
            Items = Items.Skip(startIndex).Take(length).Select(i => new SelectedItem(i.Name!, i.Name!)),
            TotalCount = 80
        };
    }

    /// <summary>
    /// 获得事件方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<EventItem> GetEvents() => new EventItem[]
    {
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
        }
    };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new() {
            Name = "ShowLabel",
            Description = Localizer["SelectsShowLabel"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new() {
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
        }
    };
}
