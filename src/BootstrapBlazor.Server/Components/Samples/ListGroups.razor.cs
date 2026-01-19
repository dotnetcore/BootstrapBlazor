// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ListGroup 示例代码
/// </summary>
public partial class ListGroups
{
    [NotNull]
    private List<Foo>? Items { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    [NotNull]
    private Foo? Value { get; set; }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(LocalizerFoo);
    }

    private static string? GetItemDisplayText(Foo item) => item.Name;

    /// <summary>
    /// GetAttributes
    /// </summary>
    /// <returns></returns>
    private AttributeItem[] GetAttributes() =>
    [
        new()
        {
            Name = "Items",
            Description = Localizer["AttrItems"],
            Type = "List<TItem>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "Value",
            Description = Localizer["AttrValue"],
            Type = "TItem",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderText",
            Description = Localizer["AttrHeaderText"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "HeaderTemplate",
            Description = Localizer["AttrHeaderTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "ItemTemplate",
            Description = Localizer["AttrItemTemplate"],
            Type = "RenderFragment",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnClickItem",
            Description = Localizer["AttrOnClickItem"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "OnDoubleClickItem",
            Description = Localizer["AttrOnDoubleClickItem"],
            Type = "Func<TItem, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new()
        {
            Name = "GetItemDisplayText",
            Description = Localizer["GetItemDisplayText"],
            Type = "Func<TItem, string>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}
