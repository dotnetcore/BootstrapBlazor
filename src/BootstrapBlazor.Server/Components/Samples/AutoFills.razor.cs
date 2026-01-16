// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
///
/// </summary>
partial class AutoFills
{
    [NotNull]
    private Foo Model1 { get; set; } = new();

    [NotNull]
    private Foo Model2 { get; set; } = new();

    [NotNull]
    private Foo Model3 { get; set; } = new();

    [NotNull]
    private Foo Model4 { get; set; } = new();

    [NotNull]
    private Foo Model5 { get; set; } = new();

    private static string? OnGetDisplayText(Foo? foo) => foo?.Name;

    [NotNull]
    private IEnumerable<Foo>? Items1 { get; set; }

    [NotNull]
    private IEnumerable<Foo>? Items2 { get; set; }

    [NotNull]
    private IEnumerable<Foo>? Items3 { get; set; }

    [NotNull]
    private IEnumerable<Foo>? Items4 { get; set; }

    [NotNull]
    private IEnumerable<Foo>? Items5 { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    private bool _isClearable = true;

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items1 = Foo.GenerateFoo(LocalizerFoo);
        Model1 = Items1.First();

        Items2 = Foo.GenerateFoo(LocalizerFoo);
        Model2 = Items2.First();

        Items3 = Foo.GenerateFoo(LocalizerFoo);
        Model3 = Items3.First();

        Items4 = Foo.GenerateFoo(LocalizerFoo);
        Model4 = Items3.First();

        Items5 = Foo.GenerateFoo(LocalizerFoo);
        Model5 = Items3.First();
    }

    private Task<IEnumerable<Foo>> OnCustomFilter(string searchText)
    {
        var items = string.IsNullOrEmpty(searchText) ? Items2 : Items2.Where(i => i.Count > 50 && i.Name!.Contains(searchText));
        return Task.FromResult(items);
    }

    private Task<IEnumerable<Foo>> OnCustomVirtulizeFilter(string searchText)
    {
        var items = string.IsNullOrEmpty(searchText) ? Items5 : Items5.Where(i => i.Name!.Contains(searchText));
        return Task.FromResult(items);
    }

    private async Task<QueryData<Foo>> OnQueryAsync(VirtualizeQueryOption option)
    {
        await Task.Delay(200);
        var items = Foo.GenerateFoo(LocalizerFoo);
        if (!string.IsNullOrEmpty(option.SearchText))
        {
            items = [.. items.Where(i => i.Name!.Contains(option.SearchText, StringComparison.OrdinalIgnoreCase))];
        }
        return new QueryData<Foo>
        {
            Items = items.Skip(option.StartIndex).Take(option.Count),
            TotalCount = items.Count
        };
    }

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
}
