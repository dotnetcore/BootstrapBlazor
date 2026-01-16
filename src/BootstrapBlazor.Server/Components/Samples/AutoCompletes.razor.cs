// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// AutoComplete 组件示例
/// </summary>
public sealed partial class AutoCompletes
{
    private Foo Model { get; set; } = new Foo() { Name = "" };

    private static List<string> StaticItems => ["1", "12", "123", "1234", "12345", "123456", "abc", "abcdef", "ABC", "aBcDeFg", "ABCDEFG"];

    private readonly List<string> _items = [];

    private IEnumerable<string> Items => _items;

    private string _value = "";
    private string _matchValue = "";
    private string _tipValue = "";
    private string _filterValue = "";
    private string _debounceValue = "";

    private static async Task<IEnumerable<string>> OnCustomFilter(string val)
    {
        await Task.Yield();
        var items = new List<string> { $"{val}@163.com", $"{val}@126.com", $"{val}@sina.com", $"{val}@hotmail.com" };
        return items;
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
}
