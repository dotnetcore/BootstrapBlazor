﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Searches
/// </summary>
public sealed partial class Searches
{
    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task<IEnumerable<string>> OnSearch(string searchText)
    {
        Logger.Log($"SearchText: {searchText}");
        return Task.FromResult<IEnumerable<string>>([$"{searchText}1", $"{searchText}12", $"{searchText}123"]);
    }

    [NotNull]
    private ConsoleLogger? ClearLogger { get; set; }

    private Task<IEnumerable<string>> OnClearSearch(string searchText)
    {
        ClearLogger.Log($"SearchText: {searchText}");
        return Task.FromResult<IEnumerable<string>>([$"{searchText}1", $"{searchText}12", $"{searchText}123"]);
    }

    [NotNull]
    private ConsoleLogger? DisplayLogger { get; set; }

    private Task<IEnumerable<string>> OnDisplaySearch(string searchText)
    {
        DisplayLogger.Log($"SearchText: {searchText}");
        return Task.FromResult<IEnumerable<string>>([$"{searchText}1", $"{searchText}12", $"{searchText}123"]);
    }

    private Task<IEnumerable<string>> OnClear(string searchText)
    {
        DisplayLogger.Log($"OnClear: {searchText}");
        return Task.FromResult<IEnumerable<string>>([$"{searchText}1", $"{searchText}12", $"{searchText}123"]);
    }

    [NotNull]
    private ConsoleLogger? KeyboardLogger { get; set; }

    private Task<IEnumerable<string>> OnKeyboardSearch(string searchText)
    {
        KeyboardLogger.Log($"SearchText: {searchText}");
        return Task.FromResult<IEnumerable<string>>([$"{searchText}1", $"{searchText}12", $"{searchText}123"]);
    }

    private Foo Model { get; set; } = new Foo() { Name = "" };

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
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
    ];
}
