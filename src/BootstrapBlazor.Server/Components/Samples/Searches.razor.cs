// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Searches
/// </summary>
public sealed partial class Searches
{
    [Inject, NotNull]
    private ToastService? ToastService { get; set; }

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

    private Foo Model { get; } = new() { Name = "" };

    private static string? OnGetDisplayText(Foo? foo) => foo?.Name;

    private async Task<IEnumerable<Foo>> OnSearchFoo(string searchText)
    {
        // 模拟异步延时
        await Task.Delay(100);
        return Enumerable.Range(1, 10).Select(i => new Foo()
        {
            Id = i,
            Name = LocalizerFoo["Foo.Name", $"{i:d4}"],
            Address = LocalizerFoo["Foo.Address", $"{Random.Shared.Next(1000, 2000)}"],
            Count = Random.Shared.Next(1, 100)
        }).ToList();
    }

    private async Task<IEnumerable<string?>> OnModelSearch(string v)
    {
        // 模拟异步延时
        await Task.Delay(100);
        return string.IsNullOrEmpty(v)
            ? Enumerable.Empty<string>()
            : Enumerable.Range(1, 10).Select(i => LocalizerFoo["Foo.Name", $"{i:d4}"].Value).ToList();
    }

    private async Task OnClickCamera(SearchContext<string?> context)
    {
        await Task.Delay(10);

        await ToastService.Information("Custom IconTemplate", "Click custom icon");
    }

    private bool _isClearable = true;
    private bool _showClearButton = false;
    private bool _showSearchButton = false;

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new() {
            Name = "NoDataTip",
            Description = Localizer["SearchesNoDataTip"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["SearchesNoDataTipDefaultValue"]
        },
        new()
        {
            Name="IsClearable",
            Description = Localizer["SearchesIsClearable"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name="ClearIcon",
            Description = Localizer["SearchesClearIcon"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name="PrefixButtonTemplate",
            Description = Localizer["SearchesPrefixButtonTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name="ButtonTemplate",
            Description = Localizer["SearchesButtonTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
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
            Name = "IsTriggerSearchByInput",
            Description = Localizer["SearchesIsTriggerSearchByInput"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
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
