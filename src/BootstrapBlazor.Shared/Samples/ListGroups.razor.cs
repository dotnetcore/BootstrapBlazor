// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

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
    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
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
            Name = "GetItemDisplayText",
            Description = Localizer["GetItemDisplayText"],
            Type = "Func<TItem, string>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}
