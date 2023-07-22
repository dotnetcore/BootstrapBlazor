// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
///
/// </summary>
partial class AutoFills
{
    [NotNull]
    private Foo Model { get; set; } = new();

    private Task OnSelectedItemChanged(Foo foo)
    {
        Model = Utility.Clone(foo);
        StateHasChanged();
        return Task.CompletedTask;
    }

    private static string OnGetDisplayText(Foo foo) => foo.Name ?? "";

    [NotNull]
    private IEnumerable<Foo>? Items { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    /// <inheritdoc/>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(LocalizerFoo);
        Model = Items.First();
    }

    private Task<IEnumerable<Foo>> OnCustomFilter(string searchText)
    {
        var items = string.IsNullOrEmpty(searchText) ? Items : Items.Where(i => i.Count > 50 && i.Name!.Contains(searchText));
        return Task.FromResult(items);
    }

    /// <summary>
    /// Get property method
    /// </summary>
    /// <returns></returns>
    private IEnumerable<AttributeItem> GetAttributes() => new[]
    {
        // TODO: move to database
        new AttributeItem() {
            Name = "DisplayCount",
            Description = Localizer["Att1"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "NoDataTip",
            Description = Localizer["Att2"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = Localizer["Def2"]
        },
        new AttributeItem() {
            Name = "IgnoreCase",
            Description = Localizer["Att3"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "IsLikeMatch",
            Description = Localizer["Att4"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = "Items",
            Description = Localizer["Att5"],
            Type = "IEnumerable<TValue>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = "Debounce",
            Description = Localizer["Att6"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "0"
        },
        new AttributeItem() {
            Name = "OnCustomFilter",
            Description = Localizer["Att7"],
            Type = "Func<string, Task<IEnumerable<TValue>>>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnGetDisplayText",
            Description = Localizer["Att8"],
            Type = "Func<TValue, string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = "OnSelectedItemChanged",
            Description = Localizer["Att9"],
            Type = "Func<TValue, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(AutoFill<Foo>.ShowDropdownListOnFocus),
            Description = Localizer["Att10"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "true"
        },
        new AttributeItem() {
            Name = "Template",
            Description = Localizer["Att11"],
            Type = "RenderFragment<TValue>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem()
        {
            Name = nameof(AutoFill<string>.SkipEnter),
            Description = Localizer["Att12"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new AttributeItem()
        {
            Name = nameof(AutoFill<string>.SkipEsc),
            Description = Localizer["Att13"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        }
    };
}
