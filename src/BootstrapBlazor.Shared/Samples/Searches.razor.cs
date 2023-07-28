// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Searches
/// </summary>
public sealed partial class Searches
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    [NotNull]
    private ConsoleLogger? ClearLogger { get; set; }

    private static IEnumerable<string> Items => new string[] { "1", "12", "123", "1234" };

    private Task OnSearch(string searchText)
    {
        Logger.Log($"SearchText: {searchText}");
        return Task.CompletedTask;
    }

    private Task OnClearSearch(string searchText)
    {
        ClearLogger.Log($"SearchText: {searchText}");
        return Task.CompletedTask;
    }

    [NotNull]
    private ConsoleLogger? DisplayLogger { get; set; }

    private Task OnDisplaySearch(string searchText)
    {
        DisplayLogger.Log($"SearchText: {searchText}");
        return Task.CompletedTask;
    }

    private Task OnClear(string searchText)
    {
        DisplayLogger.Log($"OnClear: {searchText}");
        return Task.CompletedTask;
    }

    [NotNull]
    private ConsoleLogger? KeyboardLogger { get; set; }

    private Task OnKeyboardSearch(string searchText)
    {
        KeyboardLogger.Log($"SearchText: {searchText}");
        return Task.CompletedTask;
    }

    private Foo Model { get; set; } = new Foo() { Name = "" };

    private static List<string> StaticItems => new() { "1", "12", "123", "1234", "12345", "123456", "abc", "abcdef", "ABC", "aBcDeFg", "ABCDEFG" };

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
