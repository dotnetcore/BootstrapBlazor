// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Common;
using BootstrapBlazor.Shared.Components;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// 
/// </summary>
public sealed partial class Searchs
{
    private static IEnumerable<string> Items => new string[] { "1", "12", "123", "1234" };

    [NotNull]
    private BlockLogger? Trace { get; set; }

    [NotNull]
    private BlockLogger? Trace2 { get; set; }

    [NotNull]
    private BlockLogger? Trace3 { get; set; }

    [NotNull]
    private BlockLogger? Trace4 { get; set; }

    private Task OnSearch1(string searchText)
    {
        Trace.Log($"SearchText: {searchText}");
        return Task.CompletedTask;
    }

    private Task OnSearch2(string searchText)
    {
        Trace2.Log($"SearchText: {searchText}");
        return Task.CompletedTask;
    }

    private Task OnSearch3(string searchText)
    {
        Trace3.Log($"SearchText: {searchText}");
        return Task.CompletedTask;
    }

    private Task OnSearch4(string searchText)
    {
        Trace4.Log($"SearchText: {searchText}");
        return Task.CompletedTask;
    }

    private Task OnClear(string searchText)
    {
        Trace3.Log($"OnClear: {searchText}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
            // TODO: 移动到数据库中
            new AttributeItem() {
                Name = "ChildContent",
                Description = Localizer["ChildContent"],
                Type = "RenderFragment",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "Items",
                Description = Localizer["Items"],
                Type = "IEnumerable<string>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "NoDataTip",
                Description = Localizer["NoDataTip"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = Localizer["NoDataTipDefaultValue"]
            },
            new AttributeItem()
            {
                Name="SearchButtonLoadingIcon",
                Description = Localizer["SearchButtonLoadingIcon"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-fw fa-spinner fa-spin"
            },
            new AttributeItem() {
                Name = "ClearButtonIcon",
                Description = Localizer["ChildContent"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = "fa fa-trash"
            },
            new AttributeItem() {
                Name = "ClearButtonText",
                Description = Localizer["ClearButtonText"],
                Type = "string",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem() {
                Name = "ClearButtonColor",
                Description = Localizer["ClearButtonColor"],
                Type = "Color",
                ValueList = " — ",
                DefaultValue = "Secondary"
            },
            new AttributeItem() {
                Name = "SearchButtonColor",
                Description = Localizer["SearchButtonColor"],
                Type = "Color",
                ValueList = " — ",
                DefaultValue = "Primary"
            },
            new AttributeItem() {
                Name = "IsLikeMatch",
                Description = Localizer["IsLikeMatch"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsAutoFocus",
                Description = Localizer["IsAutoFocus"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsAutoClearAfterSearch",
                Description = Localizer["IsAutoClearAfterSearch"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem() {
                Name = "IsOnInputTrigger",
                Description = Localizer["IsOnInputTrigger"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name = "IgnoreCase",
                Description = Localizer["IgnoreCase"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "true"
            },
            new AttributeItem()
            {
                Name = "ShowClearButton",
                Description = Localizer["ShowClearButton"],
                Type = "bool",
                ValueList = "true|false",
                DefaultValue = "false"
            },
            new AttributeItem()
            {
                Name="OnSearch",
                Description = Localizer["OnSearch"],
                Type = "Func<string, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            },
            new AttributeItem()
            {
                Name="OnClear",
                Description = Localizer["OnClear"],
                Type = "Func<string, Task>",
                ValueList = " — ",
                DefaultValue = " — "
            }
    };
}
