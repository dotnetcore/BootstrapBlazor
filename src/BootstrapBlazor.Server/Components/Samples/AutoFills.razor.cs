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
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "DisplayCount",
            Description = Localizer["Att1"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "NoDataTip",
            Description = Localizer["Att2"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["Def2"]
        },
        new()
        {
            Name = "IgnoreCase",
            Description = Localizer["Att3"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new()
        {
            Name = "IsLikeMatch",
            Description = Localizer["Att4"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = "Items",
            Description = Localizer["Att5"],
            Type = "IEnumerable<TValue>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Debounce",
            Description = Localizer["Att6"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new()
        {
            Name = "OnCustomFilter",
            Description = Localizer["Att7"],
            Type = "Func<string, Task<IEnumerable<TValue>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnGetDisplayText",
            Description = Localizer["Att8"],
            Type = "Func<TValue, string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnSelectedItemChanged",
            Description = Localizer["Att9"],
            Type = "Func<TValue, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        //new()
        //{
        //    Name = nameof(AutoFill<Foo>.ShowDropdownListOnFocus),
        //    Description = Localizer["Att10"],
        //    Type = "bool",
        //    ValueList = "true/false",
        //    DefaultValue = "true"
        //},
        new()
        {
            Name = "Template",
            Description = Localizer["Att11"],
            Type = "RenderFragment<TValue>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = nameof(AutoFill<string>.SkipEnter),
            Description = Localizer["Att12"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(AutoFill<string>.SkipEsc),
            Description = Localizer["Att13"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new()
        {
            Name = nameof(AutoFill<string>.IsVirtualize),
            Description = Localizer["AttrIsVirtualize"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    ];
}
