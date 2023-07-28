// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
///
/// </summary>
public sealed partial class AutoCompletes
{
    private Foo Model { get; set; } = new Foo() { Name = "" };

    private static List<string> StaticItems => new() { "1", "12", "123", "1234", "12345", "123456", "abc", "abcdef", "ABC", "aBcDeFg", "ABCDEFG" };

    private readonly List<string> _items = new();
    private IEnumerable<string> Items => _items;
    private Task OnValueChanged(string val)
    {
        _items.Clear();
        _items.Add($"{val}@163.com");
        _items.Add($"{val}@126.com");
        _items.Add($"{val}@sina.com");
        _items.Add($"{val}@hotmail.com");
        return Task.CompletedTask;
    }

    [NotNull]
    private ConsoleLogger? Logger { get; set; }

    private Task OnSelectedItemChanged(string val)
    {
        Logger.Log($"Value: {val}");
        return Task.CompletedTask;
    }

    [NotNull]
    private ConsoleLogger? GroupLogger { get; set; }

    private Task GroupOnSelectedItemChanged(string val)
    {
        GroupLogger.Log($"Value: {val}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 获得属性方法
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {

        new()
        {
            Name = "ShowLabel",
            Description = Localizer["Att1"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "ChildContent",
            Description = Localizer["Att2"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ItemTemplate",
            Description = Localizer["AttItemTemplate"],
            Type = "RenderFragment<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Items",
            Description = Localizer["Att3"],
            Type = "IEnumerable<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "NoDataTip",
            Description = Localizer["Att4"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["Att4DefaultValue"]!
        },
        new()
        {
            Name = "DisplayCount",
            Description = Localizer["Att5"],
            Type = "int?",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ValueChanged",
            Description = Localizer["Att6"],
            Type = "Action<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "IsLikeMatch",
            Description = Localizer["Att7"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "IgnoreCase",
            Description = Localizer["Att8"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "OnCustomFilter",
            Description = Localizer["Att9"],
            Type = "Func<string, Task<IEnumerable<string>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Debounce",
            Description = Localizer["Debounce"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = nameof(AutoComplete.SkipEnter),
            Description = Localizer[nameof(AutoComplete.SkipEnter)],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(AutoComplete.SkipEsc),
            Description = Localizer[nameof(AutoComplete.SkipEsc)],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(AutoComplete.OnValueChanged),
            Description = Localizer[nameof(AutoComplete.OnValueChanged)],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(AutoComplete.OnSelectedItemChanged),
            Description = Localizer[nameof(AutoComplete.OnSelectedItemChanged)],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
