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

        new() {
            Name = "ChildContent",
            Description = Localizer["SearchesChildContent"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "Items",
            Description = Localizer["SearchesItems"],
            Type = "IEnumerable<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "NoDataTip",
            Description = Localizer["SearchesNoDataTip"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SearchesNoDataTipDefaultValue"]
        },
        new()
        {
            Name="SearchButtonLoadingIcon",
            Description = Localizer["SearchesButtonLoadingIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-fw fa-spin fa-solid fa-spinner"
        },
        new() {
            Name = "ClearButtonIcon",
            Description = Localizer["SearchesClearButtonIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = "fa-solid fa-trash"
        },
        new() {
            Name = "ClearButtonText",
            Description = Localizer["SearchesClearButtonText"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = "ClearButtonColor",
            Description = Localizer["SearchesClearButtonColor"],
            Type = "Color",
            ValueList = " — ",
            DefaultValue = "Secondary"
        },
        new() {
            Name = "SearchButtonColor",
            Description = Localizer["SearchesButtonColor"],
            Type = "Color",
            ValueList = " — ",
            DefaultValue = "Primary"
        },
        new() {
            Name = "IsLikeMatch",
            Description = Localizer["SearchesIsLikeMatch"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsAutoFocus",
            Description = Localizer["SearchesIsAutoFocus"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsAutoClearAfterSearch",
            Description = Localizer["SearchesIsAutoClearAfterSearch"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = "IsOnInputTrigger",
            Description = Localizer["SearchesIsOnInputTrigger"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IgnoreCase",
            Description = Localizer["SearchesIgnoreCase"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ShowClearButton",
            Description = Localizer["SearchesShowClearButton"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name="OnSearch",
            Description = Localizer["SearchesOnSearch"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name="OnClear",
            Description = Localizer["SearchesOnClear"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
