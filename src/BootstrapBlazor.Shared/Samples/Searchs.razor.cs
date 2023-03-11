// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Searchs
/// </summary>
public sealed partial class Searchs
{
    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        // TODO: 移动到数据库中
        new AttributeItem() {
            Name = "ChildContent",
            Description = Localizer["SearchsChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["SearchsItems"],
            Type = "IEnumerable<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "NoDataTip",
            Description = Localizer["SearchsNoDataTip"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SearchsNoDataTipDefaultValue"]
        },
        new AttributeItem()
        {
            Name="SearchButtonLoadingIcon",
            Description = Localizer["SearchsButtonLoadingIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-fw fa-spin fa-solid fa-spinner"
        },
        new AttributeItem() {
            Name = "ClearButtonIcon",
            Description = Localizer["SearchsClearButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-trash"
        },
        new AttributeItem() {
            Name = "ClearButtonText",
            Description = Localizer["SearchsClearButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "ClearButtonColor",
            Description = Localizer["SearchsClearButtonColor"],
            Type = "Color",
            ValueList = " — ",
            DefaultValue = "Secondary"
        },
        new AttributeItem() {
            Name = "SearchButtonColor",
            Description = Localizer["SearchsButtonColor"],
            Type = "Color",
            ValueList = " — ",
            DefaultValue = "Primary"
        },
        new AttributeItem() {
            Name = "IsLikeMatch",
            Description = Localizer["SearchsIsLikeMatch"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsAutoFocus",
            Description = Localizer["SearchsIsAutoFocus"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsAutoClearAfterSearch",
            Description = Localizer["SearchsIsAutoClearAfterSearch"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "IsOnInputTrigger",
            Description = Localizer["SearchsIsOnInputTrigger"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = "IgnoreCase",
            Description = Localizer["SearchsIgnoreCase"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new AttributeItem()
        {
            Name = "ShowClearButton",
            Description = Localizer["SearchsShowClearButton"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name="OnSearch",
            Description = Localizer["SearchsOnSearch"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name="OnClear",
            Description = Localizer["SearchsOnClear"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
